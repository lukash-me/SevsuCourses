<template>
    <div class="container">
        <h1>Административная панель</h1>
        <div class="group">
            <h2>Преподаватели</h2>

            <table>
                <thead>
                    <tr>
                        <td>Аналитик Иван Сергеевич</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr v-if="deletedTeacher==false">
                        <td>Всезнающий Андрей Николаевич</td>
                        <td @click="openTeacherCard" class="main-info">Основная информация</td>
                        <td @click="openPasswordModal" class="password">Пароль</td>
                        <td @click="openDeleteModal" class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Креативная Марина Петровна</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Мудрый Дмитрий Александрович</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Неработающая Ксения Андреевна</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Опытный Сергей Павлович</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Понимающий Дмитрий Васильевич</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Профессионал Анна Викторовна</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    
                </thead>
            </table>
        </div>

        <div class="group">
            <h2>Менторы</h2>
            <table>
                <thead>
                    <tr>
                        <td>Васильев Николай Сергеевич</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Кузнецов Дмитрий Александрович</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Морозова Марина Петровна</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Петрова Анна Викторовна</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Смирнов Алексей Петрович</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                </thead>
            </table>
        </div>

        <div class="group">
            <h2>Студенты</h2>
            <table>
                <thead>
                    <tr>
                        <td>Беляева Екатерина Александровна</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Васильев Дмитрий Владимирович</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Васильев Николай Иванович</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Волков Артем Евгеньевич</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Гаврилов Михаил Сергеевич</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Григорьев Олег Владимирович</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Егоров Дмитрий Владимирович</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Егорова Виктория Максимовна</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Зайцева Татьяна Викторовна</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Иванов Дмитрий Владимирович</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Иванов Иван Михайлович</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Иванова Анастасия Сергеевна</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Иванова Ольга Сергеевна</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Кузнецов Николай Михайлович</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Кузнецов Юрий Сергеевич</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Кузнецова Анна Юрьевна</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Кузнецова Виктория Андреевна</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Кузнецова Мария Александровна</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Лебедева Ольга Владимировна</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Мельников Владимир Павлович</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Михайлов Юрий Николаевич</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Морозов Сергей Иванович</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Никитин Артем Вячеславович</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Новиков Александр Дмитриевич</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Новиков Алексей Иванович</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Орлов Алексей Дмитриевич</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Павлова Елизавета Олеговна</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Петров Артем Владимирович</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Петров Николай Дмитриевич</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Попов Владимир Дмитриевич</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Попов Дмитрий Алексеевич</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Попов Михаил Сергеевич</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Семенова Ирина Владимировна</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Сидоров Владимир Павлович</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Сидорова Наталья Сергеевна</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Смирнов Александр Сергеевич</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Смирнов Алексей Юрьевич</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Смирнова Елена Павловна</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Соколов Александр Петрович</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Тихонов Николай Анатольевич</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Федоров Сергей Юрьевич</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                    <tr>
                        <td>Федорова Мария Николаевна</td>
                        <td class="main-info">Основная информация</td>
                        <td class="password">Пароль</td>
                        <td class="delete">Удалить</td>
                    </tr>
                </thead>
            </table>
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
                      <td @click="openEditModal"> {{email}} </td>
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

        <div v-if="isEditModalOpen" class="overlay">
            <div class="edit">
                <h1>Введите новое значение</h1>
                <input type="text">
                <button @click="editEmail" class="save-btn">Ок</button>
            </div>
        </div>
    </div>

    <div v-if="isPasswordModalOpen" class="overlay">
        <div class="card" id="password-card">
            <span>Текущий пароль</span>
            <span @click="openEditPasswordModal">{{ password }}</span>
            <button @click="closePasswordModal" class="save-btn">Ок</button>
        </div>
    </div>

    <div v-if="isEditPasswordModalOpen" class="overlay">
        <div class="edit">
            <h1>Введите новое значение</h1>
            <input type="text">
            <button @click="editPassword" class="save-btn">Ок</button>
        </div>
    </div>

    <div v-if="isModalDeleteInfoOpen" class="overlay">
        <div class="delete-confirm">
            <div class="text-modal">
                <span>Вы <b>уверены</b>, что хотите</span>
                <span><span class="red-font">удалить</span> выбранного пользователя?</span>
            </div>
            <div class="btns-container">
                <button class="cancel-btn" @click="closeDeleteModal">Вернуться</button>
                <button class="delete-btn-big" @click="deleteUser">Удалить</button>
            </div>
        </div>
    </div>
</template>

<script>
    import { ref } from 'vue';
    import { useRouter } from 'vue-router';
    //import Cookies from "js-cookie";

    export default {
    name: 'adminPage',
    

    setup() {
        
        const router = useRouter();
        let deletedTeacher = ref(false);
        let isTeacherCardOpen = ref(false);
        let email = ref("vsandrnvch1990@mail.ru");
        let isEditModalOpen = ref(false);
        let isPasswordModalOpen = ref(false);
        let isEditPasswordModalOpen = ref(false);
        let password = ref("111");
        let isModalDeleteInfoOpen = ref(false);

        function editEmail() {
            email.value = "andrey@mail.ru"
            closeEditModal();
        }

        function openDeleteModal(){
            isModalDeleteInfoOpen.value = true;
        }

        function deleteUser() {
            deletedTeacher.value = true;
            closeDeleteModal();
        }

        function closeDeleteModal(){
            isModalDeleteInfoOpen.value = false;
        }

        function editPassword() {
            password.value = "555"
            closeEditPasswordModal();
        }

        function openEditPasswordModal() {
            isEditPasswordModalOpen.value = true;
        }

        function closeEditPasswordModal() {
            isEditPasswordModalOpen.value = false;
        }

        function closePasswordModal() {
            isPasswordModalOpen.value = false;
        }

        function openPasswordModal() {
            isPasswordModalOpen.value = true;
        }

        function openEditModal() {
            isEditModalOpen.value = true;
        }

        function closeEditModal() {
            isEditModalOpen.value = false;
        }

        function openTeacherCard() {
            isTeacherCardOpen.value = true;
        }

        function closeTeacherCard() {
            isTeacherCardOpen.value = false;
        }

        return {
        router,
        deletedTeacher,
        isTeacherCardOpen,
        email,
        editEmail,
        openEditModal,
        isEditModalOpen,
        closeEditModal,
        openTeacherCard,
        closeTeacherCard,
        isPasswordModalOpen,
        password,
        closePasswordModal,
        openPasswordModal,
        openEditPasswordModal,
        closeEditPasswordModal,
        editPassword,
        isEditPasswordModalOpen,
        openDeleteModal,
        isModalDeleteInfoOpen,
        closeDeleteModal,
        deleteUser,
        };
    },
    };
</script>

<style scoped>

    #password-card span {
        font-size: 20px;
        color: black;
        text-align: center;
        margin-top: 20px;
    }

    #password-card button{
        margin-top: 60px;
    }

    .edit {

        margin-top: 700px;
        width: 800px;
        height: 200px;

        background: white;

        border: 2px solid var(--border-color);
        box-shadow: 4px 4px 4px rgba(0, 0, 0, 0.25);
        border-radius: 20px;
        background-color: var(--course-edit-form-bckg-clr);

        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
    }

    .edit h1 {
        color: black;
        font-size: 20px;
    }

    .edit input{
        width: 90%;
        height: 50px;
        margin-top: 20px;

        margin-bottom: 20px;

        border-radius: 20px;
        border: 2px solid var(--border-color);

        color: black;
        font-size: 16px;
        padding-left: 10px;
    }

    .container {
        margin-top: 80px;
        display: flex;
        flex-direction: column;

        justify-self: center;

        width: 90%;
        min-height: 100vh;

        background-color: white;

        border-radius: 20px;
    }

    .delete-confirm .btns-container {
        margin-top: 20px;
    }

    .delete-confirm .btns-container button{
        margin-left: 20px;
    }

    .container h2 {
        text-align: center;
    }

    .container table {
        margin-bottom: 60px;
    }

    .container table td {
        padding-bottom: 8px;
    }


    .group {
        width: 96%;
        display: flex;
        flex-direction: column;
        margin-left: 20px;
    }

    .group td, h2{

        color: black;
        font-size: 16px;
    }

    .group tr:nth-child(even){
        background: rgb(241, 241, 241);
    }

    table .delete {
        color: red;
        cursor: pointer;
    }

    table .password {
        color: blue;
        cursor: pointer;
    }

    table .main-info {
        color: blue;
        cursor: pointer;
    }

    .container h1 {
        font-size: 20px;
        color: black;
        text-align: center;
        margin-top: 20px;
    }


</style>