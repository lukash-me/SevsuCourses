<template>
  <div v-if="haveNoRightsModal" class="overlay">
        <div class="delete-confirm" id="rights">
            <div class="text-modal">
                <span>У вас<span class="red-font"> нет прав</span> для</span>
                <span> <b>выполнения</b> данного действия</span>
            </div>
            <div class="btns-container">
                <button class="cancel-btn" @click="closeNoRightsModal">Увы</button>
            </div>
        </div>
    </div>
    <div>
        <div class="block">
            <h1 v-if="themeData">Theme №{{ themeData.number + 1 }}. {{ themeData.title }}</h1>

        
            <div class="img-container">
            <img src="/images/themes_01.jpg" alt="Theme Image" />
            </div>

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
            <span>{{ studentAnswer.answerText }}</span>
            <div class="status"></div>
            </div>


            <div class="btns-container">
              <button class="btn" @click="openMarkModal">Оценить работу</button>
            </div>

            <div class="container">
            <h2>Ответ ментора</h2>
            <span>{{ studentAnswer.replyText ? studentAnswer.replyText : MENTOR_REPLY_NOT_FOUND }}</span>
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
                  <textarea class="box" v-model="studentAnswer.answerText"></textarea>
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
import { STUDENT_ANSWER_NOT_FOUND, MENTOR_REPLY_NOT_FOUND } from '@/constants'
import { getAnswer, createAnswer, createReply } from '@/utils/requests/answers';
import { getTheme } from '@/utils/requests/themes';
import { getTask } from '@/utils/requests/tasks';
import { getStudentsInfo } from '@/utils/requests/students';
import { getSolution } from '@/utils/requests/solutions';

import { getRole, getId, logResultIfFailure } from '@/utils/shared/shared';
import { getAllGroupsOnCourse, getMentorGroupsOnCourse } from "@/utils/requests/groups";

export default {
  name: "TaskPage",

  setup() {
    const route = useRoute();
    const router = useRouter();
    const activeStudent = ref({});
    const themeData = ref({});
    const studentAnswer = ref({});
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

      const taskId = route.query.id;
      const taskResult = await getTask(taskId);

      if (logResultIfFailure(taskResult)) {
        return;
      }

      const themeId = taskResult.themeId;

      const result = await getTheme(themeId);

      if (logResultIfFailure(result)) {
        return;
      }

      router.push({ 
        name: "themesPage", 
        query: { id: result.courseId }
      });
    };

    watch(activeStudent, async (newStudent) => {

      const taskId = route.query.id;

      if (newStudent) {

        const studentId = newStudent.id;
        const result = await getAnswer(taskId, studentId);

        if (logResultIfFailure(result)) {

          if (result.errors[0].code!="record.not.found") {
            return
          }
        }

        studentAnswer.value.id = result.id ? result.id : null;

        if (result.answerText === null || studentAnswer.value.id === null) {
          studentAnswer.value.answerText = STUDENT_ANSWER_NOT_FOUND;
          studentAnswer.value.replyText = MENTOR_REPLY_NOT_FOUND;
          return;
        }

        if (result.replyText === null) {
          studentAnswer.value.mark = 0;
        }

        studentAnswer.value.answerText = result.answerText;
        studentAnswer.value.replyText = result.replyText ? result.replyText : MENTOR_REPLY_NOT_FOUND;
        studentAnswer.value.mark = result.mark;
      }
    });

    function closeNoRightsModal() {
      haveNoRightsModal.value = false;
    }

    function openNoRightsModal() {
      haveNoRightsModal.value = true;
    }

    return {
      form,
      formMark,
      students,
      route,
      themeData,
      studentAnswer,
      showAnswerModal,
      isMarkModalOpen,
      activeStudent,
      goToThemes,
      haveNoRightsModal,
      closeNoRightsModal,
      openNoRightsModal,
    };
  },

  async getThemeByTask(taskId) {

    const taskResult = await getTask(taskId);

    if (logResultIfFailure(taskResult)) {
      return false;
    }

    const themeId = taskResult.themeId;
    const themeResult = await getTheme(themeId);

    if (logResultIfFailure(themeResult)) {
      return false;
    }

    return themeResult;
  },

  async mounted(){

    const taskId = this.$route.query.id
    const theme  = await this.getThemeByTask(taskId);

    const courseId = theme.courseId;

    if (taskId) {

      this.themeData = { title: theme.title, number: theme.number };

      if (getRole() === "Student"){

        const studentId = getId();
        const result = await this.getStudentMainInfo(studentId);

        if (logResultIfFailure(result)) {
          return;
        }

        const studentName = result.fio;

        this.activeStudent = { id: studentId, fio: studentName };
        this.students = [{ id: studentId, fio: studentName }];

        await getAnswer(taskId, this.activeStudent.id);
        // TODO: обработка ошибки обработки ответа
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

      if (logResultIfFailure(groupsResult)) {
        return;
      }

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

        const studentsResult = await getStudentsInfo(request);

        if (logResultIfFailure(studentsResult)) {
          return;
        }

        for (const student of studentsResult){
          students.push(student);
        }
      }

      return students;
    },

    setStudentsWithAnswers() {

    },

    openAnswerModal() {

      if (getRole() != "Student" && getRole() != "Admin"){
        this.openNoRightsModal();
        return
      }

      this.showAnswerModal = true;
    },

    closeAnswerModal() {
      this.showAnswerModal = false;
    },

    async openMarkModal() {

      if (getRole() != "Mentor" & getRole() != "Admin"){
        this.openNoRightsModal();
        return
      }

      if (this.studentAnswer.id === null) {
        console.log("Нельзя оценивать без ответа студента");
        return;
      }

      this.isMarkModalOpen = true;

      const taskId = this.$route.query.id;

      const solutionResult = await getSolution(taskId);

      if (logResultIfFailure(solutionResult)) {
        return
      }

      const solution = solutionResult[0];

      this.formMark.solutionText = solution;
      this.formMark.mark = this.studentAnswer.mark;
      this.formMark.replyText = this.studentAnswer.replyText;
    },

    closeMarkModal() {
      this.isMarkModalOpen = false;
    },

    async sendAnswer() {

      const request = {
        taskId: this.$route.query.id,
        studentId: getId(),
        answerText: this.form.answerText
      }

      const result = await createAnswer();

      if (logResultIfFailure(result)) { 
        return;
      }

      this.studentAnswer.answerText = request.answerText;
      this.closeAnswerModal();
    },

    async sendReply() {

      const request = {
        replyText: this.formMark.replyText,
        mark: this.formMark.mark
      }

      const answerId = this.studentAnswer.id;

      const result = await createReply(answerId, request);

      if (logResultIfFailure(result)) {
        return;
      }

      console.log("Добавлен ответ ментора. id", result);

      this.closeMarkModal();

      this.studentAnswer.replyText = request.replyText;
      this.studentAnswer.mark = request.mark;
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