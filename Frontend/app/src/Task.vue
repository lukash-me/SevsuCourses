<template>

  <NoRightsModal v-if="haveNoRightsModal==true" @close="haveNoRightsModal=false"/>

    <div>
        <div class="block">
            <h1 v-if="theme">Theme №{{ theme.number + 1 }}. {{ theme.title }}</h1>

        
            <div class="img-container">
            <img src="/images/themes_01.jpg" alt="Theme Image" />
            </div>

            <!-- лоадер добавить -->
            <h2>{{ task.title }}</h2>

            <div class="container">
            <h2>Условие</h2>
            <span>{{ task.condition }}</span>
            </div>

            <div class="btns-container">
              <button class="btn" @click="openAnswerModal">Ответить</button>
            </div>

            <div class="selector-container">
              <span>Выбранный студент:</span>
              <select v-model="activeStudent" class="selector" name="student-selector" id="">
                <option v-for="student in students" :key="student.id" :value="student">
                  {{ student.fio }}
                </option>
              </select>
            </div>

            <div id="answer" class="container">
            <h2>Ответ студента</h2>
            <span>{{ answer.answerText }}</span>
            <div class="status"></div>
            </div>


            <div class="btns-container">
              <button class="btn" @click="openMarkModal">Оценить работу</button>
            </div>

            <div class="container">
            <h2>Ответ ментора</h2>
            <span>{{ answer ? answer.replyText : "loading..." }}</span>
            </div>

            <div class="btns-container">
              <button class="btn" @click="goToThemes">Вернуться к темам</button>
            </div>

            <!-- Модальное окно ответа студента -->
            <div v-if="showAnswerModal" class="overlay">
              <form class="answer-send-form" @submit.prevent="handleSubmit" novalidate>

                <h1>Отправить ответ</h1>

                <div class="field">
                  <span>Введите текст ответа</span>
                  <textarea v-model="form.answerText"
                  class="box"
                  ></textarea>
                </div>
                
                <div class="btns-form-container">
                  <button class="cancel-btn" @click="closeAnswerModal">Вернуться</button>
                  <button class="save-btn" @click="sendAnswer">Отправить</button>
                </div>
              </form>
            </div>

            <!-- Модальное окно ответа ментора -->

            <FormReply v-if="isMarkModalOpen" 
              :answer="answer"
              :form="formMark"
              @submit="sendReply" 
              @close="isMarkModalOpen=false" 
            />
        </div>
    </div>
</template>

<script>
import { ref, reactive, watch, onMounted } from "vue";
import { useRoute, useRouter } from "vue-router";

import NoRightsModal from '@/components/NoRightsModal.vue';
import FormReply from '@/components/FormReply.vue';

import { TasksController } from '@/controllers/tasksController';
import { ThemesController } from '@/controllers/themesController';
import { AnswersController } from '@/controllers/answersController';
import { StudentsController } from '@/controllers/studentsController';
import { SolutionsController } from '@/controllers/solutionsController';
import { GroupsController } from '@/controllers/groupsController';

import { STUDENT_ANSWER_NOT_FOUND, MENTOR_REPLY_NOT_FOUND } from '@/constants';
import { openModal, closeModal } from '@/utils/modals/modals';

import { getRole, getId, logIfFailure } from '@/utils/shared/shared';

export default {
  name: "TaskPage",

  components: {
    NoRightsModal,
    FormReply
  },

  setup() {
    const taskController = new TasksController();
    const themesController = new ThemesController();
    const answersController = new AnswersController();
    const studentsController = new StudentsController();
    const solutionsController = new SolutionsController();
    const groupsController = new GroupsController();

    const answer = ref({});

    // model. ToDo: Сделать маппинг в нужный формат
    const task = ref({});
    const theme = ref({});
    //const answer = ref({}); // Сейчас один, но в будущем несколько с пагинацией



    const route = useRoute();
    const router = useRouter();
    const activeStudent = ref({});
    const students = ref({});
    const showAnswerModal = ref(false);
    const isMarkModalOpen = ref(false);
    let haveNoRightsModal = ref(false);

    const form = reactive({
      answerText: null,
    });

    const formMark = ref({});

    const goToThemes = async () => {

      router.push({ 
        name: "themesPage", 
        query: { id: theme.value.courseId }
      });
    }

    // Это должна быть generic функция
    async function getMainInfo(controller, entityId) {

      await controller.loadStudentMainInfo(entityId);

      if (logIfFailure(controller)) {
        return
      }
    }

    async function getAnswer(controller, taskId, studentId) {
      await controller.loadAnswer(taskId, studentId);

      if (logIfFailure(controller)) {
        return;
      }
    }

    function setAnswer() {
      answer.value = answersController.answers[0];
    }

    onMounted(async () => {

      const taskId = useRoute().query.id;
      await getThemeByTask(taskId);

      const courseId = theme.value.courseId;

      if (taskId) {

        if (getRole() === "Student"){

          const studentId = getId();

          await getMainInfo(studentsController, studentId);

          const mainInfo = studentsController.students[0];
          const currentStudents = [{ id: studentId, fio: mainInfo.fio }];

          setStudents(currentStudents);

          return;
        }

        if (getRole() === "Mentor") {

          //getStudents
          //setStudents

          const mentorId = getId();

          let students = await getStudents(courseId, mentorId);
          students = dropAttestFromStudents(students);

          setStudents(students);

          return;
        }

        if (getRole() === "Teacher" || getRole() === "Admin") {

          let students = await getStudents(courseId);
          students = dropAttestFromStudents(students);
          
          setStudents(students);

          return;
        }
      } 
    })

    async function getThemeByTask(taskId) {

      await taskController.loadTask(taskId);

      if (logIfFailure(taskController)) {
        return false;
      }

      const currentTask = taskController.tasks[0];
      const themeId = currentTask.themeId;
      task.value = currentTask;

      await themesController.loadTheme(themeId);

      if (logIfFailure(themesController)) {
        return false;
      }

      const currentTheme = themesController.themes[0];
      theme.value = currentTheme;
    }

    function setStudents(currentStudents) {
      students.value = currentStudents;
      activeStudent.value = currentStudents[0];
    }

    async function getStudents(courseId, mentorId=null) {

      if (mentorId===null) {
        await groupsController.loadAllGroupsOnCourse(courseId);
      }

      if (mentorId) {
        await groupsController.loadMentorGroupsOnCourse(mentorId, courseId);
      }

      if (logIfFailure(groupsController)) {
        return;
      }

      const groups = groupsController.groups;
      const groupIds = groups.groupIds;

      const result = await getStudentsFromGroups(groupIds);
      return result;
    }

    function dropAttestFromStudents(students) {

      const result = students.map(student => {
        // eslint-disable-next-line no-unused-vars
        const { isAttest, ...rest } = student; 
        return rest;
      });

      return result;
    }

    async function getStudentsFromGroups(groupIds) {

      let students = [];

      for (const id of groupIds) {

        const request = {
          groupIds: [id]
        }

        await studentsController.loadStudentsInfo(request);

        if (logIfFailure(studentsController)) {
          return;
        }

        for (const student of studentsController.students){
          students.push(student);
        }
      }

      return students;
    }

    function openAnswerModal() {

      if (getRole() != "Student" && getRole() != "Admin"){
        // this.haveNoRightsModal = true;
        console.log(haveNoRightsModal);
        openModal(haveNoRightsModal);
        return
      }

      showAnswerModal.value = true;

      form.answerText = answer.value.answerText;
    }

    function closeAnswerModal() {
      showAnswerModal.value = false;
    }

    async function openMarkModal() {

      if (getRole() != "Mentor" & getRole() != "Admin"){
        openModal(haveNoRightsModal);
        return
      }

      if (answer.value.id === null) {
        console.log("Нельзя оценивать без ответа студента");
        return;
      }

      const taskId = route.query.id;

      await solutionsController.loadSolution(taskId);

      if (logIfFailure(solutionsController)) {
        return;
      }

      const solution = solutionsController.solutions[0];

      formMark.value.solutionText = solution;
      formMark.value.mark = answer.value.mark;
      formMark.value.replyText = answer.value.replyText;

      isMarkModalOpen.value = true;
    }

    function closeMarkModal() {
      isMarkModalOpen.value = false;
    }

    async function sendAnswer() {

      const taskId = route.query.id;
      
      const studentId = getId();

      const request = {
        taskId: taskId,
        studentId: studentId,
        answerText: form.answerText
      }

      await answersController.createAnswer(request);

      if (logIfFailure(answersController)) {
        return;
      }

      await answersController.loadAnswer(taskId, studentId);

      if (logIfFailure(answersController)) {
        return;
      }

      answer.value = answersController.answers[0];
      answer.value.replyText = MENTOR_REPLY_NOT_FOUND;

      this.closeAnswerModal();
    }

    async function sendReply(form) {

      if (form instanceof Event) {
        return;
      }

      const request = {
        replyText: form.replyText,
        mark: form.mark
      }

      const answerId = answer.value.id;

      await answersController.createReply(answerId, request);

      if (logIfFailure(answersController)) {
        return;
      }

      console.log("Добавлен ответ ментора. id", answersController.answers[0]);

      closeMarkModal();

      await getAnswer(answersController, task.value.id, activeStudent.value.id);

      if (logIfFailure(answersController)) {
        return;
      }

      setAnswer();
    }

    watch(activeStudent, async (newStudent) => {

      const taskId = route.query.id;

      if (newStudent) {

        const studentId = newStudent.id;

        await answersController.loadAnswer(taskId, studentId);

        if (answersController.errors.length != 0) {
          if (answersController.errors[0].code!="record.not.found") {
            return
          }
        }

        setAnswer();

        if (answer.value === undefined) {
          answer.value = {
            answerText: STUDENT_ANSWER_NOT_FOUND,
            replyText: MENTOR_REPLY_NOT_FOUND
          }
          return
        }

        if (answer.value.replyText === null) {
          answer.value.mark = 0;
          answer.value.replyText = MENTOR_REPLY_NOT_FOUND;
        }
      }
    });

    return {
      task,
      theme,
      answer,
      form,
      formMark,
      students,
      route,
      showAnswerModal,
      isMarkModalOpen,
      activeStudent,
      goToThemes,
      haveNoRightsModal,
      closeModal,
      taskController,
      themesController,
      answersController,
      studentsController,
      solutionsController,
      groupsController,
      getThemeByTask,
      setStudents,
      dropAttestFromStudents,
      openAnswerModal,
      sendAnswer,
      openMarkModal,
      closeAnswerModal,
      getStudents,
      sendReply,
      closeMarkModal
      
    };
  },

};
</script>




<style scoped>



.theme-title {
    margin-top: 80px;
    width: 100%;
    height: 100px;
    margin-bottom: 20px;
    text-align: center;
    display: flex;
    align-items: center;
    justify-content: center;
}

.theme-title span{
    font-size: 3rem;
    color: #fff;
}

h1 {
    font-size: 3rem;
}

h2 {
    font-size: 2rem;
}


.title {
    width: 100%;
    height: 50px;
    text-align: center;
    display: flex;
    align-items: center;
    justify-content: center;
}

.title span{
    font-size: 2rem;
    color: #fff;
}

.block {
    
    width: 98%;
    height: 2000px;
    display: flex;
    justify-content: center;
    flex-direction: column;
    align-items: center;
    justify-content: flex-start;

    margin: 0 auto;
    margin-top: 10rem;
}

.block span{
    margin-top: 2rem;
    margin-bottom: 2rem;
    font-size: 2rem;
    color: #fff;
}

.block .container{
    width: 100%;
    min-height: 200px;
    font-size: 2rem;
    color: #fff;
    text-align: justify;
    padding-left: 20px;
    padding-right: 20px;
    border: 6px solid white;
    border-radius: 20px;
    margin-bottom: 50px;
    box-shadow: 0 5px 10px #000;
}

.block .img-container {
    margin-bottom: 40px;
    width: 400px;
    height: 500px;
    border-radius: 0px;
    overflow: hidden;
}

.block .img-container img{
    width: 100%;
    height: 100%;
    object-fit:cover;
}

.btns-container {
    width: 97%;
    display: flex;
    margin-bottom: 40px;
}

.btns-container button{
    font-size: 18px;
}

.btns-form-container {

  margin-top: 40px;
  margin-bottom: 40px;
  position: relative;
  align-self: center;
  display: flex;
  justify-content: center;
  gap: 30px;
}

.block .status {
    margin-top: 30px;
    color: rgb(114, 222, 255);
    font-size: 2rem;
}

.selector-container {
  display: flex;
  align-self: flex-start;

  align-items: center;
}

.selector-container .selector {
  width: 400px;
  height: 30px;

  margin-left: 20px;
  border-radius: 10px;
}

select {
  padding-left: 5px;
  color: black;
  font-size: 14px;
}

option {
  color: #000;
  font-size: 14px;
}



</style>