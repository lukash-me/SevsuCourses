<template>
  <head>
    <meta charset="utf-8">
    <title>Все курсы</title>

  </head>

  <header>
    <div>
      <a href="index.html" class="logo">Все курсы</a>
    </div>

    <nav>
      <ul>
        <li><a href="#">Мои группы</a></li>
        <li>
          <span class="user">{{ userName }}</span>
          <ul>
            <li @click="logout"><span>Выйти</span></li>
          </ul>
        </li>
      </ul>
    </nav>
  </header>

  <router-view></router-view>
</template>

<script>
  import { useRouter } from 'vue-router';
  import Cookies from "js-cookie";

  export default{
    setup(){
      const router = useRouter();

      function logout() {
        Cookies.remove("id", { path: "/"});
        Cookies.remove("role", { path: "/"});
        router.push({ name: 'loginPage' });
      }

      return { logout }
    },

    data() {
        return {
          userName: null,
        };
    },

    mounted() {
      this.getUserName()
    },

    methods: {
      async getUserName() {
        const id = Cookies.get("id")
        const role = Cookies.get("role")
        try {
          const response = await fetch(`http://localhost:5036/${role}/fio/${id}`);
          if (!response.ok) {
              throw new Error('Network response was not ok ' + response.statusText);
          }
          let fio = await response.text();
          fio = fio.split(" ");
          fio = fio[0] + " " + fio[1][0] + ". " + fio[2][0] + ".";
          this.userName = fio;
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
    z-index: 999;
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
}

.course-card .image img{
    width: 100%;
    height: 100%;
    object-fit:cover;
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
    flex: 0 1 auto;
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

</style>
