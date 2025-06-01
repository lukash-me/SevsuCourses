<template>

  <NoRightsModal v-if="haveNoRightsModal" @close="haveNoRightsModal=false"/>

  <div v-if="haveNoRightsModal" class="overlay">
        <div class="delete-confirm" id="rights">
            <div class="text-modal">
                <span>У вас<span class="red-font"> нет прав</span> для</span>
                <span> <b>выполнения</b> данного действия</span>
            </div>
            <div class="btns-container">
                <button class="cancel-btn" @click="closeModal(haveNoRightsModal)">Увы</button>
            </div>
        </div>
    </div>
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
            <span>{{ answer.replyText ? answer.replyText : MENTOR_REPLY_NOT_FOUND }}</span>
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
            <div v-if="isMarkModalOpen" class="overlay">
              <form class="reply-send-form" @submit.prevent="handleSubmit" novalidate>
                <h1>Оценивание ответа студента</h1>

                <div class="field">
                  <span>Ответ студента</span>
                  <textarea class="box" v-model="answer.answerText"></textarea>
                </div>

                <div class="field">
                  <span>Эталонное решение</span>
                  <textarea class="box" v-model="formMark.solutionText"></textarea>
                </div>

                <div class="field">
                  <span>Ваш комментарий</span>
                  <textarea class="box" v-model="formMark.replyText"></textarea>
                </div>

                <div class="field">
                  <span>Оценить выполнение</span>
                  <input class="markBox" v-model="formMark.mark" type="number" name="mark">
                </div>
                
                <div class="btns-form-container">
                  <button class="cancel-btn" @click="closeMarkModal"> Вернуться</button>
                  <button class="save-btn" @click="sendReply">Отправить</button>
                </div>
              </form>
            </div>
        </div>
    </div>
</template>

<script>
import { ref, reactive, watch } from "vue";
import { useRoute, useRouter } from "vue-router";
import NoRightsModal from '@/components/NoRightsModal.vue';

import { TasksController } from '@/controllers/tasksController';
import { ThemesController } from '@/controllers/themesController';
import { AnswersController } from '@/controllers/answersController';
import { StudentsController } from '@/controllers/studentsController';
import { SolutionsController } from '@/controllers/solutionsController';

import { STUDENT_ANSWER_NOT_FOUND, MENTOR_REPLY_NOT_FOUND } from '@/constants';
import { openModal, closeModal } from '@/utils/modals/modals';

import { getRole, getId, logIfFailure } from '@/utils/shared/shared';
import { getAllGroupsOnCourse, getMentorGroupsOnCourse } from "@/utils/requests/groups";

export default {
  name: "TaskPage",

  setup() {
    const taskController = new TasksController();
    const themesController = new ThemesController();
    const answersController = new AnswersController();
    const studentsController = new StudentsController();
    const solutionsController = new SolutionsController();

    // model. ToDo: Сделать маппинг в нужный формат
    const task = ref({});
    const theme = ref({});
    const answer = ref({}); // Сейчас один, но в будущем несколько с пагинацией



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

    const formMark = reactive({
      solutionText: null,
      replyText: null,
      mark: 0
    });

    const goToThemes = async () => {

      router.push({ 
        name: "themesPage", 
        query: { id: theme.value.courseId }
      });
    };

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

        answer.value = answersController.answers[0];

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
      NoRightsModal,
      taskController,
      themesController,
      answersController,
      studentsController,
      solutionsController
      
    };
  },

  async mounted(){

    const taskId = this.$route.query.id;
    await this.getThemeByTask(taskId);

    const courseId = this.theme.courseId;

    if (taskId) {

      if (getRole() === "Student"){

        const studentId = getId();

        await this.studentsController.loadStudentMainInfo(studentId);

        if (logIfFailure(this.studentsController)) {
          return
        }

        const mainInfo = this.studentsController.students[0];

        this.activeStudent = { id: studentId, fio: mainInfo.fio };
        this.students = [{ id: studentId, fio: mainInfo.fio }];

        await this.answersController.loadAnswer(taskId, studentId);

        if (logIfFailure(this.answersController)) {
          return;
        }

        this.answer = this.answersController.answers[0];

        return;
      }

      if (getRole() === "Mentor") {

        const mentorId = getId();

        let students = await this.getStudents(courseId, mentorId);
        students = this.dropAttestFromStudents(students);

        this.setStudents(students);

        return;
      }

      if (getRole() === "Teacher" || getRole() === "Admin") {

        let students = await this.getStudents(courseId);
        students = this.dropAttestFromStudents(students);
        
        this.setStudents(students);

        return;
      }
    } 
  },

  methods: {

    async getThemeByTask(taskId) {

      await this.taskController.loadTask(taskId);

      if (logIfFailure(this.taskController)) {
        return false;
      }

      const task = this.taskController.tasks[0];

      const themeId = task.themeId;
      this.task = task;

      await this.themesController.loadTheme(themeId);

      if (logIfFailure(this.themesController)) {
        return false;
      }

      const theme = this.themesController.themes[0];
      this.theme = theme;
    },

    setStudents(students) {
      this.students = students;
      this.activeStudent = this.students[0];
    },

    async getStudents(courseId, mentorId=null) {

      let groupsResult;

      if (mentorId===null) {
        groupsResult = await getAllGroupsOnCourse(courseId);
      }

      if (mentorId) {
        groupsResult = await getMentorGroupsOnCourse(mentorId, courseId);
      }

      // if (logResultIfFailure(groupsResult)) {
      //   return;
      // }

      const groupIds = groupsResult.groupIds;

      const result = await this.getStudentsFromGroups(groupIds);
      return result;
    },

    dropAttestFromStudents(students) {

      const result = students.map(student => {
        // eslint-disable-next-line no-unused-vars
        const { isAttest, ...rest } = student; 
        return rest;
      });

      return result;
    },

    async getStudentsFromGroups(groupIds) {

      let students = [];

      for (const id of groupIds) {

        const request = {
          groupIds: [id]
        }

        await this.studentsController.loadStudentsInfo(request);

        if (logIfFailure(this.studentsController)) {
          return;
        }

        for (const student of this.studentsController.students){
          students.push(student);
        }
      }

      return students;
    },

    openAnswerModal() {

      if (getRole() != "Student" && getRole() != "Admin"){
        // this.haveNoRightsModal = true;
        console.log(this.haveNoRightsModal);
        openModal(this.haveNoRightsModal);
        return
      }

      this.showAnswerModal = true;
      this.form.answerText = this.answer.answerText;
    },

    closeAnswerModal() {
      this.showAnswerModal = false;
    },

    async openMarkModal() {

      if (getRole() != "Mentor" & getRole() != "Admin"){
        this.haveNoRightsModal=true;
        openModal(this.haveNoRightsModal);
        return
      }

      if (this.answer.id === null) {
        console.log("Нельзя оценивать без ответа студента");
        return;
      }

      this.isMarkModalOpen = true;

      const taskId = this.$route.query.id;

      await this.solutionsController.loadSolution(taskId);

      if (logIfFailure(this.solutionsController)) {
        return;
      }

      const solution = this.solutionsController.solutions[0];

      this.formMark.solutionText = solution;
      this.formMark.mark = this.answer.mark;
      this.formMark.replyText = this.answer.replyText;
    },

    closeMarkModal() {
      this.isMarkModalOpen = false;
    },

    async sendAnswer() {

      const taskId = this.$route.query.id;
      const studentId = getId();

      const request = {
        taskId: taskId,
        studentId: studentId,
        answerText: this.form.answerText
      }

      await this.answersController.createAnswer(request);

      if (logIfFailure(this.answersController)) {
        return;
      }

      await this.answersController.loadAnswer(taskId, studentId);

      if (logIfFailure(this.answersController)) {
        return;
      }

      this.answer = this.answersController.answers[0];
      this.answer.replyText = MENTOR_REPLY_NOT_FOUND;

      this.closeAnswerModal();
    },

    async sendReply() {

      const request = {
        replyText: this.formMark.replyText,
        mark: this.formMark.mark
      }

      const answerId = this.answer.id;

      await this.answersController.createReply(answerId, request);

      if (logIfFailure(this.answersController)) {
        return;
      }

      console.log("Добавлен ответ ментора. id", this.answersController.answers[0]);

      this.closeMarkModal();

      await this.answersController.loadAnswer(this.task.id, this.activeStudent.id);

      if (logIfFailure(this.answersController)) {
        return;
      }

      this.answer = this.answersController.answers[0];
    },
  }
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