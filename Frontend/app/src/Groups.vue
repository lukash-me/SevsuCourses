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

    <div class="container">
      <h1>Алгоритмы с нуля</h1>
      <div class="groupName">
        <h2>Весна 2025</h2>
        <span>Удалить группу</span>
      </div>
      <span>Обучается</span>
      <span>15.04.2025 - 15.06.2025</span>
      
      <table>
        <tbody>
          <tr>
            <td>Преподаватель</td>
            <td class="name">Всезнающий Андрей Николаевич</td>
          </tr>
          <tr>
            <td>Ментор</td>
            <td @click="openMentorCard" class="name">Смирнов Алексей Петрович</td>
          </tr>
        </tbody>
      </table>

      <h2>Студенты</h2>
      <table>
        <tbody>
          <tr>
            <td @click="openStudentCard" class="name">1. Васильев Дмитрий Владимирович</td>
            <td>Аттестован</td>
            <td @click="openStudentProgress" class="progress">Смотреть прогресс</td>
            <td class="delete">Удалить</td>
          </tr>
          <tr>
            <td class="name">2. Васильев Николай Иванович</td>
            <td>Аттестован</td>
            <td class="progress">Смотреть прогресс</td>
            <td class="delete">Удалить</td>
          </tr>
          <tr>
            <td class="name">3. Иванов Дмитрий Владимирович</td>
            <td>Аттестован</td>
            <td class="progress">Смотреть прогресс</td>
            <td class="delete">Удалить</td>
          </tr>
          <tr>
            <td class="name">4. Кузнецов Юрий Сергеевич</td>
            <td>Аттестован</td>
            <td class="progress">Смотреть прогресс</td>
            <td class="delete">Удалить</td>
          </tr>
          <tr>
            <td class="name">5. Попов Владимир Дмитриевич</td>
            <td>Аттестован</td>
            <td class="progress">Смотреть прогресс</td>
            <td class="delete">Удалить</td>
          </tr>
          <tr>
            <td class="name">6. Смирнов Александр Сергеевич</td>
            <td>Аттестован</td>
            <td class="progress">Смотреть прогресс</td>
            <td class="delete">Удалить</td>
          </tr>
          <tr>
            <td class="name">7. Смирнов Алексей Юрьевич</td>
            <td>Аттестован</td>
            <td class="progress">Смотреть прогресс</td>
            <td class="delete">Удалить</td>
          </tr>
          <tr v-if="studentAdded">
            <td class="name">8. Волков Артем Евгеньевич</td>
            <td>Не аттестован</td>
            <td class="progress">Смотреть прогресс</td>
            <td @click="openDeleteStudentModal" class="delete">Удалить</td>
          </tr>
        </tbody>
      </table>

      <span @click="openAddStudentModal" class="addStudent">Добавить студента</span>
    </div>
    
    <div v-if="groupAdded == true" class="container">
      <h1>Алгоритмы с нуля</h1>
      <div class="groupName">
        <h2>Лето 2025</h2>
        <span @click="openDeleteGroupModal">Удалить группу</span>
      </div>
      <span>Формируется</span>
      <span>23.03.2025 - 23.08.2025</span>
      
      <table>
        <tbody>
          <tr>
            <td>Преподаватель</td>
            <td>Всезнающий Андрей Николаевич</td>
          </tr>
          <tr>
            <td>Ментор</td>
            <td>Смирнов Алексей Петрович</td>
          </tr>
        </tbody>
      </table>

      <h2>Студенты</h2>
      <table>
        <tbody>
          <tr>
            <td>1. Орлов Алексей Дмитриевич</td>
            <td>Не аттестован</td>
            <td class="progress">Смотреть прогресс</td>
            <td class="delete">Удалить</td>
          </tr>
          <tr>
            <td>2. Морозов Сергей Иванович</td>
            <td>Не аттестован</td>
            <td class="progress">Смотреть прогресс</td>
            <td class="delete">Удалить</td>
          </tr>
        </tbody>
      </table>

      <span class="addStudent">Добавить студента</span>
    </div>

    <div class="btns-container">
      <button @click="addGroup" class="btn">Создать группу</button>
    </div>

    <div v-if="addGroupOpen" class="overlay">
      <form class="course-edit-form" @submit.prevent="handleSubmit" novalidate>
          
          <h1>Группа</h1>

          <div class="field">
              <span>Название*</span>
              <input
              id="title"
              v-model="name"
              type="text" 
              class="box"
              placeholder="Введите название.."
              aria-errormessage="title-errors"
              title=""
              required
              />
              <span class="field__errors" id="title-errors"></span>
          </div>

          <div class="field">
            <span>Курс</span>
            <select class="selector" name="course-selector" id="">
              <option value="asdf" disabled selected hidden>Выберите курс..</option>
              <option v-for="course in courses" :key="course" :value="course">
                {{ course }}
              </option>
            </select>
          </div>

          <div class="field">
            <span>Ментор</span>
            <select class="selector" name="mentor-selector" id="">
              <option value="asdf" disabled selected hidden>Выберите ментора..</option>
              <option v-for="mentor in mentors" :key="mentor" :value="mentor">
                {{ mentor }}
              </option>
            </select>
          </div>

          <table class="dates">
            <tbody>
              <tr>
                <td>Дата начала обучения</td>
                <td>
                  <input class="markBox" type="date" name="startDate">
                </td>
              </tr>
              <tr>
                <td>Дата окончания обучения</td>
                <td><input class="markBox" type="date" name="endDate"></td>
              </tr>
            </tbody>
          </table>

          <h2>Студенты</h2>
          <table>
            <tbody>
              <tr v-if="firstStudentAdded == true">
                <td>1. Орлов Алексей Дмитриевич</td>
                <td class="delete">Удалить</td>
              </tr>
              <tr v-if="secondStudentAdded == true">
                <td>2. Морозов Сергей Иванович</td>
                <td class="delete">Удалить</td>
              </tr>
            </tbody>
          </table>

          <div class="field">
            <select @change="addStudents" class="selector" name="students-selector" id="">
              <option value="" disabled selected hidden>Выберите очередного студента..</option>
              <option v-for="student in students"  :key="student" :value="student">
                {{ student }}
              </option>
            </select>
          </div>

          <div class="btns-container">
              <button class="cancel-btn" @click="closeGroupModal">Отменить</button>
              <button class="save-btn" @click="createGroup">Сохранить</button>
          </div>
      </form>
    </div>

    <div v-if="studentProgressOpen" class="overlay">
      <div class="studentProgress">
        <h1>Сведения об успеваемости</h1>
        <h2>Васильев Дмитрий Владимирович</h2>

        <div class="progressContainer">
          <h2>Тема 1 - Жадные алгоритмы и их применение</h2>
          <table>
            <thead>
              <tr>
                <td>Название задачи</td>
                <td>Статус</td>
                <td>Высшая оценка</td>
                <td>Дата отправки</td>
                <td>Дата проверки</td>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>Быстрая сортировка</td>
                <td>Проверено</td>
                <td>80</td>
                <td>2024-10-19 10:23:54.000</td>
                <td>2024-10-20 10:23:54.000</td>
              </tr>
              <tr>
                <td>Поиск в ширину (BFS)</td>
                <td>Отправлено</td>
                <td>-</td>
                <td>2024-11-05 12:45:00.000</td>
                <td>-</td>
              </tr>
              <tr>
                <td>Алгоритм Крускала</td>
                <td>Проверено</td>
                <td>75</td>
                <td>2024-11-08 14:12:30.000</td>
                <td>2024-11-09 14:12:30.000</td>
              </tr>
              <tr>
                <td>Реализация хэш-таблицы</td>
                <td>Проверено</td>
                <td>85</td>
                <td>2024-11-12 09:30:00.000</td>
                <td>2024-11-13 09:30:00.000</td>
              </tr>
            </tbody>
          </table>

          <h2>Тема 2 - Динамическое программирование</h2>
          <table>
            <thead>
              <tr>
                <td>Название задачи</td>
                <td>Статус</td>
                <td>Высшая оценка</td>
                <td>Дата отправки</td>
                <td>Дата проверки</td>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>Сортировка вставками</td>
                <td>Проверено</td>
                <td>80</td>
                <td>2024-11-15 11:45:00.000</td>
                <td>2024-11-16 11:45:00.000</td>
              </tr>
            </tbody>
          </table>

          <h2>Тема 3 - Алгоритмы поиска и сортировки</h2>
          <table>
            <thead>
              <tr>
                <td>Название задачи</td>
                <td>Статус</td>
                <td>Высшая оценка</td>
                <td>Дата отправки</td>
                <td>Дата проверки</td>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>Двоичный поиск</td>
                <td>Проверено</td>
                <td>92</td>
                <td>2024-11-20 10:00:00.000</td>
                <td>2024-11-21 10:00:00.000</td>
              </tr>
            </tbody>
          </table>

          <h2>Тема 4 - Графовые алгоритмы и их анализ</h2>
          <table>
            <thead>
              <tr>
                <td>Название задачи</td>
                <td>Статус</td>
                <td>Высшая оценка</td>
                <td>Дата отправки</td>
                <td>Дата проверки</td>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>Алгоритм Дейкстры</td>
                <td>Не отправлено</td>
                <td>88</td>
                <td>2024-11-25 13:20:00.000</td>
                <td>2024-11-26 13:20:00.000</td>
              </tr>
            </tbody>
          </table>
        </div>

        <button @click="closeStudentProgress" class="save-btn">Ок</button>
      </div>
    </div>

    <div v-if="addStudentOpen" class="overlay">
        <form class="course-edit-form" id="addStudentWindow" @submit.prevent="handleSubmit" novalidate>
          
          <h1>Добавить студента</h1>

          <div class="field">
            <select class="selector" name="students-selector" id="">
              <option value="" disabled selected hidden>Выберите очередного студента..</option>
              <option v-for="student in students"  :key="student" :value="student">
                {{ student }}
              </option>
            </select>
          </div>

          <div class="btns-container">
              <button class="cancel-btn" @click="closeAddStudentModal">Отменить</button>
              <button class="save-btn" @click="addStudent">Сохранить</button>
          </div>
        </form>
    </div>

    <div v-if="isModalStudentDeleteInfoOpen" class="overlay">
        <div class="delete-confirm">
            <div class="text-modal">
                <span>Вы <b>уверены</b>, что хотите</span>
                <span><span class="red-font">удалить</span> выбранного студента?</span>
            </div>
            <div class="btns-container">
                <button class="cancel-btn" @click="closeDeleteStudentModal">Вернуться</button>
                <button class="delete-btn-big" @click="deleteStudent">Удалить</button>
            </div>
        </div>
    </div>

    <div v-if="isModalGroupDeleteInfoOpen" class="overlay">
        <div class="delete-confirm">
            <div class="text-modal">
                <span>Вы <b>уверены</b>, что хотите</span>
                <span><span class="red-font">удалить</span> выбранную группу?</span>
            </div>
            <div class="btns-container">
                <button class="cancel-btn" @click="closeDeleteGroupModal">Вернуться</button>
                <button class="delete-btn-big" @click="deleteGroup">Удалить</button>
            </div>
        </div>
    </div>

    <div v-if="isStudentCardOpen" class="overlay">
        <div class="card">      
            <div class="upper">
              <div class="image">
                <img src="/images/student.jpg" alt="">
              </div>
              
              <div class="main-info">
                <h1>Студент</h1>
                <h2>Васильев Дмитрий Владимирович</h2>
                <table>
                  <tbody>
                    <tr>
                      <td>email</td>
                      <td>dmitriy.vasilev48@gmail.com</td>
                    </tr>
                    <tr>
                      <td>Телефон</td>
                      <td>+79499508685</td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </div>

            <button @click="closeStudentCard" class="save-btn">Ок</button>
        </div>
    </div>

    <div v-if="isMentorCardOpen" class="overlay">
        <div class="card">
          <div class="upper">
              <div class="image">
                <img src="/images/student.jpg" alt="">
              </div>
              
              <div class="main-info">
                <h1>Ментор</h1>
                <h2>Смирнов Алексей Петрович</h2>
                <table>
                  <tbody>
                    <tr>
                      <td>Образование</td>
                      <td>Кандидат технических наук</td>
                    </tr>
                    <tr>
                      <td>email</td>
                      <td>smirnov@example.com</td>
                    </tr>
                    <tr>
                      <td>Телефон</td>
                      <td>+79162345678</td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </div>

            <div class="description">
              <h2>Описание</h2>
              <p>Эксперт в области алгоритмов и структур данных с более чем 10-летним опытом разработки и оптимизации кода.</p>
            </div>

            <button @click="closeMentorCard" class="save-btn">Ок</button>
        </div>
    </div>

</template>

<script>

import Cookies from 'js-cookie';
import { ref } from 'vue';
import { useRouter } from 'vue-router';
//import Cookies from "js-cookie";

export default {
  name: 'GroupsPage',

  setup() {
    
    const router = useRouter();
    let studentAdded = ref(false);
    let groupAdded = ref(false);
    let firstStudentAdded = ref(false);
    let secondStudentAdded = ref(false);
    let addGroupOpen = ref(false);
    let studentProgressOpen = ref(false);
    let addStudentOpen = ref(false);
    let isStudentCardOpen = ref(false);
    let isMentorCardOpen = ref(false);
    let name = ref();
    let isModalStudentDeleteInfoOpen = ref(false);
    let isModalGroupDeleteInfoOpen = ref(false);

    const students = ['Беляева Екатерина Александровна', 'Васильев Дмитрий Владимирович','Васильев Николай Иванович','Волков Артем Евгеньевич','Гаврилов Михаил Сергеевич','Григорьев Олег Владимирович','Егоров Дмитрий Владимирович','Егорова Виктория Максимовна','Зайцева Татьяна Викторовна','Иванов Дмитрий Владимирович','Иванов Иван Михайлович','Иванова Анастасия Сергеевна','Иванова Ольга Сергеевна','Кузнецов Николай Михайлович','Кузнецов Юрий Сергеевич','Кузнецова Анна Юрьевна','Кузнецова Виктория Андреевна','Кузнецова Мария Александровна','Лебедева Ольга Владимировна','Мельников Владимир Павлович','Михайлов Юрий Николаевич','Морозов Сергей Иванович','Никитин Артем Вячеславович','Новиков Александр Дмитриевич','Новиков Алексей Иванович','Орлов Алексей Дмитриевич','Павлова Елизавета Олеговна','Петров Артем Владимирович','Петров Николай Дмитриевич','Попов Владимир Дмитриевич','Попов Дмитрий Алексеевич','Попов Михаил Сергеевич','Семенова Ирина Владимировна','Сидоров Владимир Павлович','Сидорова Наталья Сергеевна','Смирнов Александр Сергеевич','Смирнов Алексей Юрьевич','Смирнова Елена Павловна','Соколов Александр Петрович','Тихонов Николай Анатольевич','Федоров Сергей Юрьевич','Федорова Мария Николаевна']
    const mentors = ['Смирнов Алексей Петрович','Петрова Анна Викторовна','Васильев Николай Сергеевич','Кузнецов Дмитрий Александрович','Морозова Марина Петровна'];
    const courses = ['Алгоритмы с нуля'];

    let haveNoRightsModal = ref(false);

    function openNoRightsModal(){
        haveNoRightsModal.value = true;
    }

    function closeNoRightsModal(){
        haveNoRightsModal.value = false;
    }

    function openAddStudentModal (){

      if(Cookies.get("role") != "Teacher" && Cookies.get("role") != "admin") {
        this.openNoRightsModal();
        return;
      }

      addStudentOpen.value = true;
    }

    function openStudentCard() {
      isStudentCardOpen.value = true;
    }

    function closeStudentCard() {
      isStudentCardOpen.value = false;
    }

    function openMentorCard() {
      isMentorCardOpen.value = true;
    }

    function closeMentorCard() {
      isMentorCardOpen.value = false;
    }

    function closeAddStudentModal() {
      addStudentOpen.value = false;
    }

    function openStudentProgress (){

      

      studentProgressOpen.value = true;
    }

    function closeStudentProgress (){
      studentProgressOpen.value = false;
    }

    function addStudent(){

      addStudentOpen.value = false;
      studentAdded.value = true;
    }

    function openDeleteStudentModal() {

      if(Cookies.get("role") != "Teacher" && Cookies.get("role") != "admin") {
        this.openNoRightsModal();
        return;
      }

      isModalStudentDeleteInfoOpen.value = true;
    }

    function closeDeleteStudentModal() {
      isModalStudentDeleteInfoOpen.value = false;
    }

    function openDeleteGroupModal() {
      isModalGroupDeleteInfoOpen.value = true;
    }

    function closeDeleteGroupModal() {
      isModalGroupDeleteInfoOpen.value = false;
    }

    function deleteStudent(){
      isModalStudentDeleteInfoOpen.value = false;
      studentAdded.value = false;
    }

    function addGroup() {
      if(Cookies.get("role") != "Teacher" && Cookies.get("role") != "admin") {
        this.openNoRightsModal();
        return;
      }
      addGroupOpen.value = true;
    }

    function createGroup() {
      addGroupOpen.value = false;
      groupAdded.value = true;
    }

    function addFirstStudent() {
      firstStudentAdded.value = true;
    }

    function addSecondStudent() {
      secondStudentAdded.value = true;
    }

    function addStudents() {
      if (firstStudentAdded.value == false){
        addFirstStudent();
        return;
      }
      if (secondStudentAdded.value == false) {
        addSecondStudent();
        return;
      }
    }

    function closeGroupModal() {
      addGroupOpen.value = false;
    }

    function deleteGroup() {
      isModalGroupDeleteInfoOpen.value = false;
      groupAdded.value = false;
    }


    return {
      router,
      isStudentCardOpen,
      isMentorCardOpen,
      isModalGroupDeleteInfoOpen,
      studentAdded,
      groupAdded,
      firstStudentAdded,
      secondStudentAdded,
      students,
      mentors,
      courses,
      addGroupOpen,
      name,
      isModalStudentDeleteInfoOpen,
      studentProgressOpen,
      addStudentOpen,
      addStudent,
      deleteStudent,
      addGroup,
      deleteGroup,
      addSecondStudent,
      addFirstStudent,
      createGroup,
      closeGroupModal,
      addStudents,
      openStudentProgress,
      closeStudentProgress,
      openAddStudentModal,
      closeAddStudentModal,
      openDeleteStudentModal,
      closeDeleteStudentModal,
      openDeleteGroupModal,
      closeDeleteGroupModal,
      openStudentCard,
      closeStudentCard,
      openMentorCard,
      closeMentorCard,
      haveNoRightsModal,
      openNoRightsModal,
      closeNoRightsModal,
    };

    
  },
};
   
</script>

<style scoped>

  table .name {
    cursor: pointer;
  }

  .card {
    width: 800px;
    min-height: 400px;

    background-color: #fff;

    display: flex;
    flex-direction: column;
    

    border: 2px solid var(--border-color);
    box-shadow: 4px 4px 4px rgba(0, 0, 0, 0.25);
    border-radius: 20px;
    background-color: var(--course-edit-form-bckg-clr);
  }

  .card .upper {
    display: flex;
    flex-direction: row;
  }

  .card .description {
    display: flex;
    flex-direction: column;
  }

  .card .description h2{
    text-align: center;
    margin-bottom: 0px;
  }

  .card .description p{
    color: black;
    font-size: 16px;
    margin-left: 10px;
  }

  .card .upper h2 {
    margin-bottom: 30px;
  }

  .card td {
    font-size: 16px;
  }

  .card .main-info {
    width: 70%;

    display: flex;
    flex-direction: column;

    justify-content: center;
    align-items: center;
  }

  .card button {
    align-self: center;
    margin-top: 20px;
    margin-bottom: 20px;
  }

  .card .image {
    margin-left: 10px;
    margin-top: 10px;
    height: 250px;
    width: 200px;
    background-color: #9b9b9b;
    justify-content: center;
    border-radius: 14px 14px 0px 0px;
    overflow: hidden;
    display: flex;
    justify-content: left;
    position: relative;
}

.card .image img{
    width: 100%;
    height: 100%;
    object-fit:cover;
}

  .delete-confirm .btns-container {
    gap: 20px;
  }

  .studentProgress {
    width: 1100px;
    min-height: 800px;

    background-color: #fff;

    display: flex;
    justify-content: center;
    align-items: center;

    flex-direction: column;

    border: 2px solid var(--border-color);
    box-shadow: 4px 4px 4px rgba(0, 0, 0, 0.25);
    border-radius: 20px;
    background-color: var(--course-edit-form-bckg-clr);
  }

  #addStudentWindow {
    height: 250px;
  }
  #addStudentWindow h1{
    margin-bottom: 40px;
  }

  #addStudentWindow .btns-container{
    margin-top: 150px;
  }

  .studentProgress table {
    width: 100%;
    margin-left: 20px;
  }

  .studentProgress table td{
    width: 20%;
  }

  .studentProgress .save-btn {
    margin-top: 20px;
    margin-bottom: 20px;
  }

  .dates {
    margin-top: 20px;
  }

  .dates td {
    font-size: 16px;
  }

  .dates input {
    width: 150px;
    height: 40px;
    border: 2px solid var(--border-color);
    box-shadow: 4px 4px 4px rgba(0, 0, 0, 0.25);
    border-radius: 10px;
    background-color: var(--course-edit-form-bckg-clr);
    color: black;
    padding-left: 8px;
    font-size: 16px;
  }

  .course-edit-form {
    margin-top: -500px;
    height: 800px;
    position: relative;
  }

  .course-edit-form .field select {
    width: 700px;
    height: 40px;
    border: 2px solid var(--border-color);
    border-radius: 10px;
    box-shadow: 4px 4px 4px rgba(0, 0, 0, 0.25);
    color: black;
    font-size: 16px;
    padding-left: 8px;
    background-color: var(--course-edit-form-bckg-clr);
    margin-bottom: 10px;
  }

  .course-edit-form .field select option {
    color: black;
    font-size: 16px;
  }

  .course-edit-form .btns-container {
    gap: 40px;
    position: absolute;
    margin-top: 700px;
  }

  .course-edit-form h2, table {
    margin-bottom: 20px;
  }

  .container {
    margin: 0 auto;
    margin-top: 100px;

    width: 80%;
    min-height: 630px;

    display: flex;
    flex-direction: column;
    align-items: center;
    gap: 10px;

    background: #fff;
    color: black;

    border-radius: 22px;
  }

  table td{
    padding-bottom: 10px;
    padding-right: 50px;
  }

  h1, h2, span, td {
    color: black;
    font-size: 14px;
  }

  h1 {
    margin-top: 20px;
    font-size: 20px;
  }

  h2 {
    font-size: 16px;
  }

  .container table {
    color: black;
  }

  .delete {
    color: red;
    cursor: pointer;
  }

  .progress {
    color: blue;
    cursor: pointer;
  }

  .addStudent {
    margin-top: 20px;
    color: green;
    cursor: pointer;
  }

  .btns-container {
    width: 100%;
    height: 100px;

    display: flex;
    justify-content: center;
    align-items: center;
  }

  .groupName {
    width: 400px;
    display: flex;
    justify-content: center;
    align-items: center;
    position: relative;

  }

  .groupName span{
    
    display: flex;
    justify-content: center;
    align-items: center;
    position: absolute;
    margin-left: 240px;

    color: red;
    cursor: pointer;
  }



</style>