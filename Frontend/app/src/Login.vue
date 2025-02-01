<template>
    <div class="container">

        <h1>Вход на платформу</h1>

        <form @submit.prevent="handleSubmit" novalidate>

            <div class="field">
                <span>Почта</span>
                <input 
                id="email"
                v-model="email"
                type="text"
                class="box"
                placeholder="Введите почту.."
                aria-errormessage="email-errors"
                title=""
                required
                />
                <span class="field__errors" id="email-errors"></span>
            </div>

            <div class="field">
                <span>Пароль</span>
                <input
                id="password"
                v-model="password" 
                type="text" 
                class="box"
                placeholder="Введите пароль.."
                aria-errormessage="password-errors"
                title=""
                required
                />
                <span class="field__errors" id="phone-errors"></span>
            </div>

            <div class="role-container">

                <div class="role" 
                    v-for="item in roles"
                    :key="item"
                    :style="{ 
                        background: role === item ? 'rgb(114, 222, 255)' : '',
                        color: 'black' 
                    }"
                    @click="role = item"
                    >
                    {{ item  }}
                </div>
            </div>

            <div class="button-container">
                <button type="submit">Войти</button>
            </div>
        </form>
    </div>

</template>

<script>

import { ref } from 'vue';
import { useRouter } from 'vue-router';
import Cookies from "js-cookie";

export default {
  name: 'LoginPage',

  setup() {
    const router = useRouter();
    const email = ref('');
    const password = ref('');
    const role = ref('');
    const roles = ['Студент', 'Ментор', 'Учитель'];

    const fetchData = async (endpoint, emailValue, passwordValue) => {
      const data = { email: emailValue, password: passwordValue };
      const requestOptions = {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
      };

      try {
        const response = await fetch(`http://localhost:5036/${endpoint}/login`, requestOptions);
        if (!response.ok) {
          throw new Error('Network response was not ok: ' + response.statusText);
        }
        return await response.json();
      } catch (error) {
        console.error('Fetch operation failed:', error);
        return null;
      }
    };

    const handleSubmit = async () => {
      if (!role.value) {
        alert('Выберите роль!');
        return;
      }

      let endpoint;
      switch (role.value) {
        case 'Студент':
          endpoint = 'Student';
          break;
        case 'Ментор':
          endpoint = 'Mentor';
          break;
        case 'Учитель':
          endpoint = 'Teacher';
          break;
        default:
          console.log('Неизвестная роль');
          return;
      }

      const id = await fetchData(endpoint, email.value, password.value);

      if (id) {
        Cookies.set("id", id, { expires: 1, path: "/" });
        Cookies.set("role", endpoint, { expires: 1, path: "/" });
        router.push({ name: 'coursesPage' });
      } 
      else {
        alert('Ошибка входа. Проверьте данные.');
      }
    };

    return {
      email,
      password,
      role,
      roles,
      handleSubmit,
    };
  },
};
   
</script>

<style scoped>
* {
    font-size: 20px;
}

h1 {
    font-size: 30px;
    color: rgb(114, 222, 255);
}

.container {
    margin-top: 80px;
    width: 100%;
    height: 900px;
    display: flex;
    justify-content: center;
    flex-direction: column;
    align-items: center;
}

.container form .field .box{
    height: 6rem;
    width: 100%;
    margin: 1rem 0rem;
    border-radius: .5rem;
    padding: .8rem;
    -webkit-appearance: none;
    color: #000;
}

.container form .field {
    width: 600px;
    margin-bottom: 10px;
}

.container .button-container {
    margin-top: 40px;
    width: 100%;
    display: flex;
    justify-content: center;
    margin-bottom: 40px;
}

.container form button {
    width: 250px;
    height: 80px;
    background: #f4f4f4;
    border: 2px solid #1f2d6b;
    border-radius: 40px;
    display: flex;
    justify-content: center;
    align-items: center;
    font-size: 18px;
    color: #1f2d6b;
    cursor: pointer;
    margin-right: 20px;
}

.container form .role-container{
    margin-top: 30px;
    width: 100%;
    display: flex;
    justify-content: space-between;
    align-items: center;
}

.container form .role-container .role{
    width: 33.3%;
    height: 80px;
    display: flex;
    justify-content: center;
    align-items: center;
    background: #fff;
    cursor: pointer;
    border: 1px solid black;
}

.container form .role-container .role span{
    color: #000;
}

</style>