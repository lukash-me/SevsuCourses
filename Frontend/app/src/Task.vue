<template>
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

            <div class="button-container">
            <button @click="openAnswerModal">Ответить</button>
            </div>

            <div id="answer" class="container">
            <h2>Ответ студента</h2>
            <span>{{ studentAnswer.answerText }}</span>
            <div class="status">Ожидает проверки / оценка</div>
            </div>

            <div class="button-container">
            <button @click="openMarkModal">Оценить работу</button>
            </div>

            <div class="container">
            <h2>Ответ ментора</h2>
            <span>{{ studentAnswer.replyText }}</span>
            </div>

            <div class="button-container">
            <button @click="prevTask">Предыдущее задание</button>
            <button @click="nextTask">Следующее задание</button>
            </div>

            <div class="button-container">
            <button @click="goToThemes">Вернуться к темам</button>
            </div>

            <!-- Модальное окно ответа -->
            <div v-if="showAnswerModal" class="shadow">
            <div class="modal">
                <span>Введите текст ответа</span>
                <textarea v-model="answerText"></textarea>
                <div class="button-container">
                <button @click="closeAnswerModal">Вернуться к задаче</button>
                <button @click="sendAnswer">Отправить</button>
                </div>
            </div>
            </div>
        </div>
    </div>
</template>

<script>
import { ref, onMounted } from "vue";
import { useRoute, useRouter } from "vue-router";

export default {
  name: "TaskPage",

  setup() {
    const route = useRoute();
    const router = useRouter();

    const task = ref({});
    const themeData = ref({});
    const studentAnswer = ref({});
    const showAnswerModal = ref(false);
    const answerText = ref("");

    const fetchTask = async (taskId) => {
      try {
        const response = await fetch(`http://localhost:5036/Task/${taskId}`);
        if (!response.ok) throw new Error("Failed to fetch task");
        task.value = await response.json();
      } 
      catch (error) {
        console.error("Error fetching task:", error);
      }
    };

    const fetchThemeData = async (themeId) => {
      try {
        const response = await fetch(`http://localhost:5036/Theme/${themeId}`);
        if (!response.ok) throw new Error("Failed to fetch theme data");
        const data = await response.json();
        themeData.value = { title: data.title, number: data.number };
      } catch (error) {
        console.error("Error fetching theme data:", error);
      }
    };

    const fetchAnswer = async (taskId, studentId) => {
      try {
        const response = await fetch(
          `http://localhost:5036/Answer/taskId=${taskId}&studentId=${studentId}`
        );
        if (!response.ok) throw new Error("Failed to fetch answer");
        studentAnswer.value = await response.json();
      } catch (error) {
        console.error("Error fetching answer:", error);
      }
    };

    const sendAnswer = async () => {

        if (!studentAnswer.value) {
            console.error("Ошибка: studentAnswer не определён");
            return;
        }

        const data = { text: answerText.value };
        try {
            const response = await fetch(
                `http://localhost:5036/Answer/${studentAnswer.value.id}/answerText`,
                {
                    method: "PUT",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify(data),
                }
            );
            if (!response.ok) throw new Error("Failed to send answer");

            const updatedAnswer = await fetchAnswer(route.query.id, getCookie("id"));
            if (updatedAnswer) {
                studentAnswer.value = updatedAnswer; 
            }
            closeAnswerModal();
        } 
        catch (error) {
            console.error("Error sending answer:", error);
        }
    };

    const openAnswerModal = () => (showAnswerModal.value = true);
    const closeAnswerModal = () => (showAnswerModal.value = false);

    const prevTask = () => {
      // Логика для перехода к предыдущему заданию
    };

    const nextTask = () => {
      // Логика для перехода к следующему заданию
    };

    const goToThemes = () => {
      router.push({ name: "themesPage" });
    };

    const getCookie = (name) => {
      const cookies = document.cookie.split("; ");
      for (const cookie of cookies) {
        const [key, value] = cookie.split("=");
        if (key === name) return value;
      }
      return null;
    };

    onMounted(async () => {
      const taskId = route.query.id;
      const studentId = getCookie("id");
      if (taskId) {
        await fetchTask(taskId);
        await fetchThemeData(task.value.themeId);
        await fetchAnswer(taskId, studentId);
      }
    });

    return {
      task,
      themeData,
      studentAnswer,
      showAnswerModal,
      answerText,
      openAnswerModal,
      closeAnswerModal,
      sendAnswer,
      prevTask,
      nextTask,
      goToThemes,
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

.button-container {
    width: 97%;
    display: flex;
    margin-bottom: 40px;
}

.block .status {
    margin-top: 30px;
    color: rgb(114, 222, 255);
    font-size: 2rem;
}

button {
    width: 250px;
    height: 80px;
    background: #f4f4f4;
    border-radius: 40px;
    display: flex;
    justify-content: center;
    align-items: center;
    font-size: 18px;
    color: #1f2d6b;
    margin-right: 20px;
    box-shadow: 0 5px 10px #000;
    cursor: pointer;
}

.shadow .modal {
    height: 800px;
    width: 1400px;
    background: linear-gradient(to right, #141e30, #243b55);
    position: fixed;
    display: flex;
    left: 10%;
    top: 10%;
    overflow: hidden;
    z-index: 900;
    border: 4px solid white;
    border-radius: .5rem;
    flex-direction: column;
}

.shadow .modal textarea{
    align-self: center;
    height: 70%;
    width: 90%;
    border-radius: 6px;
    color: #000;
    font-size: 14px;
    padding: 0;
}

.shadow .modal .button-container{
    margin-top: 40px;
    margin-bottom: 40px;
    position: relative;
    align-self: center;
    display: flex;
    justify-content: center;
}
  
.shadow::before {
    content: '';
    position: absolute;
    top: -10%;
    left: -10%;
    width: 110%;
    height: 330%;
    background: rgba(0, 0, 0, 0.7); 
    pointer-events: auto; 
}
  
.shadow .modal span{
    margin-top: 40px;
    margin-bottom: 20px;
    margin-left: 5%;
    font-size: 2rem;
}
  


</style>