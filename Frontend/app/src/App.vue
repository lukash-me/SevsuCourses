<template>
  <head>
    <meta charset="utf-8">
    <title>Все курсы</title>

  </head>

  <header>
    <div>
      <a @click="toCoursesPage" href="" class="logo">Все курсы</a>
    </div>

    <nav>
      <ul>
        <li @click="toAdmin"><a>Админ-панель</a></li>
        <li @click="toGroupPage"><a>Мои группы</a></li>
        <li>
          <span @click="openTeacherCard" class="user">{{ userName }}</span>
          <ul>
            <li @click="logout"><span>Выйти</span></li>
          </ul>
        </li>
      </ul>
    </nav>
  </header>

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

  <div v-if="isTeacherCardOpen" class="overlay">
        <div class="card">
          <div class="upper">
              <div class="image">
                <img src="/images/student.jpg" alt="">
              </div>
              
              <div class="main-info">
                <h1>Преподаватель</h1>
                <h2>Всезнающий Андрей Николаевич</h2>
                <table>
                  <tbody>
                    <tr>
                      <td>Образование</td>
                      <td>Канд. технических наук</td>
                    </tr>
                    <tr>
                      <td>email</td>
                      <td>vsandrnvch1990@mail.ru</td>
                    </tr>
                    <tr>
                      <td>Телефон</td>
                      <td>+79784554676</td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </div>

            <div class="description">
              <h2>Описание</h2>
              <p>Меня зовут Андрей Николаевич, я закончил аспирантуру в своем университете, защитил кандидатскую и сейчас учу уму-разуму студентов на этом курсе</p>
            </div>

            <button @click="closeTeacherCard" class="save-btn">Ок</button>
        </div>
    </div>

  <router-view></router-view>
</template>

<script>
  import { useRouter } from 'vue-router';
  import Cookies from "js-cookie";
  import { ref } from "vue";

  export default{


    setup(){
      const router = useRouter();
      let userName = ref();
      let isTeacherCardOpen = ref(false);

      function logout() {
        Cookies.remove("id", { path: "/"});
        Cookies.remove("role", { path: "/"});
        userName.value = "";
        router.push({ name: 'loginPage' });
      }

      function toGroupPage() {

        const userId = Cookies.get("id") || null;
        const userRole = Cookies.get("role") || null;

        if (userId == null || userRole == null) {
          console.log("Не авторизован");
          return
        }
        else {
          router.push({ name: 'groupsPage' });
        }
      }

      function toCoursesPage() {
        const userId = Cookies.get("id") || null;
        const userRole = Cookies.get("role") || null;

        if (userId == null || userRole == null) {
          console.log("Не авторизован");
          return
        }
        else {
          router.push({ name: 'coursesPage' });
        }
      }

      function toAdmin() {
        const userId = Cookies.get("id") || null;
        const userRole = Cookies.get("role") || null;

        if (userId == null || userRole == null) {
          console.log("Не авторизован");
          return
        }
        else {
          if (userRole == "admin")
          router.push({ name: 'adminPage' });
          else {
            this.openNoRightsModal();
          }
        }


      }

      function openTeacherCard() {
        isTeacherCardOpen.value = true;
      }

      function closeTeacherCard() {
        isTeacherCardOpen.value = false;
      }

      let haveNoRightsModal = ref(false);

      function openNoRightsModal(){
          haveNoRightsModal.value = true;
      }

      function closeNoRightsModal(){
          haveNoRightsModal.value = false;
      }



      return { 
        logout, 
        toGroupPage, 
        userName,
        isTeacherCardOpen,
        toCoursesPage, 
        openTeacherCard,
        closeTeacherCard,
        toAdmin,
        haveNoRightsModal,
        openNoRightsModal,
        closeNoRightsModal
      }
    },

    async mounted() {
      await this.setUserName();
    },

    methods: {
      async setUserName() {
        if (Cookies.get("id")) {
          this.userName = await this.getUserName();
        }
      },

    async getUserName() {

      const id = Cookies.get("id");
      const role = Cookies.get("role");

      if (role == "admin"){
        return "Администратор";
      }

      try {
        const response = await fetch(`http://localhost:5036/${role}/main-info/${id}`);
        if (!response.ok) {
            throw new Error('Network response was not ok ' + response.statusText);
        }

        const data = await response.json();
        let fio = await data.fio;
        
        fio = fio.split(" ");
        fio = fio[0] + " " + fio[1][0] + ". " + fio[2][0] + ".";

        return fio;
      } 
      catch (error) {
        console.error('There was a problem with the fetch operation:', error);
      }
    }
    },

    checkLogin() {
      const userId = Cookies.get("id") || null;
      const userRole = Cookies.get("role") || null;

      if (userId == null || userRole == null) {
        this.router.push({ name: 'loginPage' });
      }
      else {
        return
      } 
    },

    

    
  }
</script>

<style>
@import url('https://fonts.googleapis.com/css2?family=Comfortaa:wght@300..700&display=swap');
@import url('https://fonts.googleapis.com/css2?family=Dela+Gothic+One&family=Tomorrow:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,100;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&display=swap');

* {
    padding: 0;
    box-sizing: border-box;
    border: 0;
    outline: 0;
  
    font-family: "Comfortaa", sans-serif;
    font-size: 62.5%;
    color: #fff;
  
    list-style: none;
    text-decoration: none;
  }

  header{
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    background: #fff;
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 0 8%;
    box-shadow: 0 5px 10px #000;
    z-index: 1200;
  }

  header nav ul li{
    position: relative;
    float: left;
  }
  
  header nav ul li a{
    padding: 15px;
    color: #000;
    font-size: 18px;
    display: block;
    transition: .3s;

    cursor: pointer;
  }

  header nav ul li a:hover{
    background: #16222A;
    color: #fff;
  }

  header nav ul li span{
    padding: 15px;
    color: #000;
    font-size: 18px;
    display: block;
    cursor: pointer;
  }
  
  header nav ul li .active-link{
    padding: 15px;
    color: #fff;
    background-color: #16222A;
    font-size: 18px;
    display: block;
    cursor: default;
  }
  
  header .logo{
    font-size: 20px;
    font-weight: 900;
    color: #000;
    transition: .5s;
  }
  
  header .logo:hover{
    transform: scale(1.1);
  }

  body {
    background: linear-gradient(to right, #141e30, #243b55);
  }

  nav ul li ul {
    position: absolute;
    left: 0;
    width: 180px;
    height: 50px;
    background: #fff;
    display: none;
  }
  
  nav ul li ul li {
    width: 100%;
    border: 1px solid rgba(0, 0, 0, 0.1);
    margin: 0px;
  }

  header nav ul li ul li span:hover{
    background-color: #ffdfe2;
  }

  nav ul li ul li span{
    color: red;
  }
  
  nav ul li:hover > ul {
    display: initial;
  }

.cards-row {
    display: flex;
    gap: 10px;
}

.course-card {
    height: 60rem;
    width: 40rem;
    background-color: #fff;
    display: flex;
    flex-direction: column;
    align-items: center;
    border-radius: 14px;
    cursor: pointer;
}

.course-card .image {
    margin-top: 1%;
    height: 50%;
    width: 98%;
    background-color: #9b9b9b;
    justify-content: center;
    border-radius: 14px 14px 0px 0px;
    overflow: hidden;
    display: flex;
    justify-content: left;
    position: relative;

    z-index: 990;
}

.course-card .image img{
    width: 100%;
    height: 100%;
    object-fit:cover;

    z-index: 990;
}

.course-card .card-text{
    text-align: center;
    margin: 1%;
}

.course-card .card-text h1{
    color: black;
    font-size: 1.8rem;
}

.course-card .card-text span{
    color: black;
    font-size: 1.8rem;
}

.course-add-card {
    height: 60rem;
    width: 40rem;
    background-color: #fff;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    border-radius: 14px;
}

.course-add-card .add-button {
    height: 98%;
    width: 98%;
    background-color: #9b9b9b;
    display: flex;
    flex-direction: column;
    align-items: center;
    border-radius: 14px;
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

    position: absolute;
    z-index: 999;
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

.card table td{
    padding-bottom: 10px;
    padding-right: 50px;
  }

.main-info h1{
  color: black;
  font-size: 14px;
}
.card h2{
  color: black;
  font-size: 14px;
}
.main-info span{
  color: black;
  font-size: 14px;
}
.main-info td{
  color: black;
  font-size: 14px;
}

.card h1 {
  margin-top: 20px;
  font-size: 20px;
}

.card h2 {
  font-size: 16px;
}



</style>
