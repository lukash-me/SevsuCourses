<template>

    <div class="course-title">
        <span>{{ courseTitle }}</span>
    </div>

    <div v-for="theme in themes" :key="theme.id" class="container">
      <h2>{{ theme.title }}</h2>
      <div class="img-container">
        <img :src="themeImage" alt="Theme Image">
      </div>
      <p class="theme-text">{{ theme.text }}</p>

      <div class="tasks">
        <div 
          v-for="(task, index) in theme.tasks" 
          :key="task.id" 
          class="task"
          @click="goToTask(task.id)"
        >
          Task №{{ index + 1 }} - {{ task.title }}
        </div>
      </div>
    </div>

    <div v-if="isModalFormOpen" class="overlay">
        <form class="theme-edit-form" @submit.prevent="handleSubmit" novalidate>
            
            <h1>Тема</h1>
            

            <div class="field">
                <span>Изображение</span>
                <input 
                id="image"
                v-model="form.image"
                type="text"
                class="box"
                placeholder="Укажите название изображения.."
                aria-errormessage="image-errors"
                title=""
                />
                <span class="field__errors" id="image-errors"></span>
            </div>

            <div class="field">
                <span>Название*</span>
                <input
                id="title"
                v-model="form.title"
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
                <span>Основной текст</span>
                <textarea
                id="description"
                v-model="form.description"
                type="text" 
                class="box"
                placeholder="Текст темы.."
                aria-errormessage="description-errors"
                title=""></textarea>
                <span class="field__errors" id="description-errors"></span>
            </div>

            <div class="btns-container">
                <button class="cancel-btn" @click="closeForm">Отменить</button>
                <button class="save-btn" @click="saveTheme">Сохранить</button>
            </div>
        </form>
    </div>

    <button class="add-theme-btn" @click="toCreateTheme">Добавить тему</button>

</template>

<script>
    import { reactive, ref } from 'vue';
    import { useRoute, useRouter } from 'vue-router';

    export default {
            name: "ThemesPage",
        setup() {
            const courseTitle = ref('');
            const themes = ref([]);
            const route = useRoute();
            const router = useRouter();
            const themeImage = '/images/themes_01.jpg';
            let isModalFormOpen = ref(false);
            const form = reactive({
                themeId: null,
                image: null,
                title: null,
                description: null,
                method: null
            })

            return { courseTitle, themes, route, router, themeImage, isModalFormOpen, form};
        },

        async mounted() {

            const courseId = this.$route.query.id;
                if (courseId) {
                    await this.getCourseTitle(courseId);
                    await this.getThemes(courseId);
                } 
                else {
                    console.error('No course ID provided');
                }
        },

        methods: {

            async getCourseTitle(courseId) {
                try {
                    const response = await fetch(`http://localhost:5036/Course/title-teacher/${courseId}`);
                    if (!response.ok) {
                        throw new Error('Failed to fetch course');
                    }
                    const data = await response.json();
                    this.courseTitle = data.title;
                } catch (error) {
                    console.error('Error fetching course:', error);
                }
            },

            async getThemes(courseId) {
                try {
                    const response = await fetch(`http://localhost:5036/Theme/all/${courseId}`);
                    if (!response.ok) {
                        throw new Error('Failed to fetch themes');
                    }
                    const data = await response.json();

                    const enrichedThemes = await Promise.all(
                        data.map(async (theme) => {
                            const tasks = await this.fetchTasks(theme.id);
                            return { ...theme, tasks };
                        })
                    );
                    this.themes = enrichedThemes.sort((a, b) => a.number - b.number);
                } 
                catch (error) {
                    console.error('Error fetching themes:', error);
                }
            },

            async fetchTasks(themeId) {
                try {
                    const response = await fetch(`http://localhost:5036/Task/theme/${themeId}`);
                    if (!response.ok) {
                        throw new Error('Failed to fetch tasks');
                    }
                    return await response.json();
                } 
                catch (error) {
                    console.error('Error fetching tasks:', error);
                    return [];
                }
            },

            goToTask(taskId) {
                this.router.push({ name: 'taskPage', query: { id: taskId } });
            },

            toCreateTheme() {  
                this.isModalFormOpen = true
            },

            async createTheme(request) {
                try {
                    const response = await fetch(`http://localhost:5036/Theme/${this.$route.query.id}`, {
                        method: "POST",
                        headers: {
                            "Content-Type": "application/json",
                        },
                        body: JSON.stringify(request),
                    });
                    if (!response.ok) {
                        throw new Error('Network response was not ok ' + response.statusText);
                    }

                    const result = await response.json();
                    console.log("Тема успешно создана:", result);

                    return result;
                } 
                catch (error) {
                    console.error('There was a problem with the fetch operation:', error);
                }
            },

            async saveTheme(){

                const request = {
                    photo: this.form.image,
                    title: this.form.title,
                    text: this.form.description
                }

                await this.createTheme(request)

                this.themes.push(request);
                
                this.closeForm();
            },

            closeForm() {
                this.isModalFormOpen = false
            }
        }
    };


</script>

<style scoped>

    .theme-text{
        font-size: 2rem;
    }

    h2{
        font-size: 3rem;
    }

    .course-title {
        margin-top: 80px;
        width: 100%;
        height: 100px;
        margin-bottom: 20px;
        border: 2px solid white;
        text-align: center;
        display: flex;
        align-items: center;
        justify-content: center;
    }

    .course-title span{
        font-size: 3rem;
        color: #fff;
    }

    .container {
        width: 100%;
        height: 900px;
        border: 2px solid white;
        display: flex;
        justify-content: center;
        flex-direction: column;
        align-items: center;
        justify-content: flex-start;
    }

    .container span{
        margin-top: 2rem;
        margin-bottom: 2rem;
        font-size: 3rem;
        color: #fff;
    }

    .container .text{
        margin-top: 2rem;
        font-size: 2rem;
        color: #fff;
    }

    .container .task{
        margin-top: 2rem;
        font-size: 2rem;
        color: #fff;
        cursor: pointer;
    }

    .container .img-container {
        width: 100%;
        height: 200px;
        border-radius: 0px;
        overflow: hidden;
    }

    .container .img-container img{
        width: 100%;
        height: 100%;
        object-fit:cover;
    }

    .btns-container {
        margin-top: 15px;
        display: flex;
        gap: 35px;
    }
</style>