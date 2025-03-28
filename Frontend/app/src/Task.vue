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
            <span>{{ studentAnswer.replyText ? studentAnswer.replyText : "Ментор пока не оценил ответ" }}</span>
            </div>

            <div class="btns-container">
              <button class="btn" @click="goToThemes">Вернуться к темам</button>
            </div>

            <!-- Модальное окно ответа -->
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
import Cookies from "js-cookie";

export default {
  name: "TaskPage",

  setup() {
    const route = useRoute();
    const router = useRouter();
    const activeStudent = ref({});

    const task = ref({});
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

    const goToThemes = () => {
      router.push({ name: "themesPage" });
    };

    async function fetchAnswer(taskId, studentId) {
      try {
        const response = await fetch(
          `http://localhost:5036/Answer/last/taskId=${taskId}&studentId=${studentId}`
        );
        if (!response.ok) {
          studentAnswer.value = {"answerText": "Студент еще не предоставил ответ на этот вопрос"}
          throw new Error("Failed to fetch answer");
        }
        studentAnswer.value = await response.json();

      } catch (error) {
        console.error("Error fetching answer:", error);
      }
    }

    watch(activeStudent, async (newStudent) => {

      const taskId = route.query.id;

      if (newStudent) {
        await fetchAnswer(taskId, newStudent.id);
      }
    });

    function closeNoRightsModal() {
      haveNoRightsModal.value = false;
    }

    function openNoRightsModal() {
      haveNoRightsModal.value = true;
    }

    return {
      task,
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
      fetchAnswer,
      haveNoRightsModal,
      closeNoRightsModal,
      openNoRightsModal,
    };
  },

  async mounted(){
    const taskId = this.$route.query.id;
    await this.fetchTask(taskId);

    const theme = await this.fetchThemeData(this.task.themeId);

      if (taskId) {

          this.themeData = { title: theme.title, number: theme.number };

          if (this.getRole() === "Student"){

            const studentId = this.getId();
            const studentName = await this.getStudentName(studentId);

            this.activeStudent = { id: studentId, fio: studentName};
            this.students = [{ id: studentId, fio: studentName}];

            await this.fetchAnswer(taskId, this.activeStudent.id);
          }

          else if (this.getRole() === "Mentor") {

            const mentorId = this.getId();

            const groups = await this.getMentorCourseGroups(mentorId, theme.courseId);

            let studentsData = [];

            for (const groupId of groups.groupIds) {
              const stdts = await this.getStudentsInfo(groupId);

              for (const student of stdts){
                studentsData.push(student);
              }
            }
            
            const students = studentsData.map(({ isAttest, ...rest }) => rest); // eslint-disable-line no-unused-vars

            this.students = students;

            this.activeStudent = this.students[0];
          }
      } 
      else {
          console.error('No task ID provided');
    }
  },

  methods: {

    openAnswerModal() {

      if (Cookies.get("role") != "Student" && Cookies.get("role") != "admin"){
        this.openNoRightsModal();
        return
      }

      this.showAnswerModal = true;
    },

    closeAnswerModal() {
      this.showAnswerModal = false;
    },

    async openMarkModal() {

      if (Cookies.get("role") != "Mentor" & Cookies.get("role") != "admin"){
        this.openNoRightsModal();
        return
      }

      this.isMarkModalOpen = true;

      const taskId = this.$route.query.id;
      const solution = await this.getSolution(taskId);
      this.formMark.solutionText = solution;
    },

    closeMarkModal() {
      this.isMarkModalOpen = false;
    },

    getId() {
      return Cookies.get("id");
    },

    getRole() {
      return Cookies.get("role");
    },

    async getStudentsInfo(groupId){
      try {
        const response = await fetch(`http://localhost:5036/Student/all-id-fio-status/${groupId}`);
        if (!response.ok) {
            throw new Error('Network response was not ok ' + response.statusText);
        }

        const data = await response.json();
        return data;

      } 
      catch (error) {
        console.error('There was a problem with the fetch operation:', error);
      }
    },

    async getMentorCourseGroups(mentorId, courseId) {
      try {
        const response = await fetch(`http://localhost:5036/Group/${mentorId}&${courseId}`);
        if (!response.ok) {
            throw new Error('Network response was not ok ' + response.statusText);
        }

        const data = await response.json();
        return data;

      } 
      catch (error) {
        console.error('There was a problem with the fetch operation:', error);
      }
    },

    async getStudentIds(studentId) {

      try {
        const response = await fetch(`http://localhost:5036/Student/main-info/${studentId}`);
        if (!response.ok) {
            throw new Error('Network response was not ok ' + response.statusText);
        }

        const data = await response.json();
        return data.fio;

      } 
      catch (error) {
        console.error('There was a problem with the fetch operation:', error);
      }
    },
    
    async getStudentName(studentId) {

      try {
        const response = await fetch(`http://localhost:5036/Student/main-info/${studentId}`);
        if (!response.ok) {
            throw new Error('Network response was not ok ' + response.statusText);
        }

        const data = await response.json();
        return data.fio;

      } 
      catch (error) {
        console.error('There was a problem with the fetch operation:', error);
      }
    },

    async fetchTask(taskId) {
      try {
        const response = await fetch(`http://localhost:5036/Task/${taskId}`);
        if (!response.ok) throw new Error("Failed to fetch task");
        this.task = await response.json();
      } 
      catch (error) {
        console.error("Error fetching task:", error);
      }
    },

    async fetchThemeData(themeId) {

      try {
        const response = await fetch(`http://localhost:5036/Theme/${themeId}`);
        if (!response.ok) throw new Error("Failed to fetch theme data");
        const data = await response.json();

        return data
        
      } catch (error) {
        console.error("Error fetching theme data:", error);
      }
    },

    async getSolution(taskId) {
      try {
        const response = await fetch(`http://localhost:5036/Solution/answer/${taskId}`);
        if (!response.ok) {
            throw new Error('Network response was not ok ' + response.statusText);
        }

        const data = await response.json();
        return data[0];

      } 
      catch (error) {
        console.error('There was a problem with the fetch operation:', error);
      }
    },

    async sendAnswer() {

      if (["Mentor", "Teacher"].includes(this.getRole())){
        console.log("Нельзя за эту роль");
        return;
      }

      const request = {
        taskId: this.$route.query.id,
        studentId: this.getId(),
        answerText: this.form.answerText
      }

      try {
          const response = await fetch(
              `http://localhost:5036/Answer`,
              {
                  method: "POST",
                  headers: { "Content-Type": "application/json" },
                  body: JSON.stringify(request),
              }
          );
          if (!response.ok) throw new Error("Failed to send answer");

          this.studentAnswer.answerText = request.answerText;

          this.closeAnswerModal();
      } 
      catch (error) {
          console.error("Error sending answer:", error);
      }
    },

    async sendReply(){

      const request = {
        replyText: this.formMark.replyText,
        mark: this.formMark.mark
      }
      
      try {

          console.log(this.formMark.replyText);
          console.log(this.formMark.mark);

          const response = await fetch(
              `http://localhost:5036/Answer/${this.studentAnswer.id}/reply-mark`,
              {
                  method: "PUT",
                  headers: { "Content-Type": "application/json" },
                  body: JSON.stringify(request),
              }
          );
          if (!response.ok) throw new Error("Failed to send answer");

          this.studentAnswer.answerText = request.answerText;

          this.closeMarkModal();
      } 
      catch (error) {
          console.error("Error sending answer:", error);
      }
    }


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
    margin-top: 10rem;
    width: 98%;
    height: 2000px;
    display: flex;
    justify-content: center;
    flex-direction: column;
    align-items: center;
    justify-content: flex-start;
    justify-self: center;
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