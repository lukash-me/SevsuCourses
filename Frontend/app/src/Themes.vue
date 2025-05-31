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
    <div class="course-title">
        <span>{{ courseTitle }}</span>
    </div>

    <div v-for="theme in themes" :key="theme.id" class="container">
        <div class="theme-title">
            <h2>{{ theme.title }}</h2>
            <div class="theme-btns-container">
                <div class="edit-btn" @click="toEditTheme(theme.id)">Редактировать</div>
                <div class="delete-btn" @click="toDeleteTheme(theme.id)">Удалить</div>
            </div>
        </div>
      
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
          <span>Задача №{{ index + 1 }} - {{ task.title }}</span>
          <button class="delete-btn" @click="toDeleteTask($event, task.id)">Удалить</button>
        </div>
        <button class="add-task-btn" @click="toCreateTask(theme.id)">Добавить задачу</button>
      </div>
    </div>

    <div v-if="isModalFormOpen" class="overlay">
        <formTheme class="theme-edit-formTheme" @submit.prevent="handleSubmit" novalidate>
            
            <h1>Тема</h1>
            

            <div class="field">
                <span>Изображение</span>
                <input 
                id="image"
                v-model="formTheme.image"
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
                v-model="formTheme.title"
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
                v-model="formTheme.description"
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
        </formTheme>
    </div>

    <div v-if="isModalDeleteInfoOpen" class="overlay">
        <div class="delete-confirm">
            <div class="text-modal">
                <span>Вы <b>уверены</b>, что хотите</span>
                <span><span class="red-font">удалить</span> выбранную тему?</span>
            </div>
            <div class="btns-container">
                <button class="cancel-btn" @click="closeDeleteModal">Вернуться</button>
                <button class="delete-btn-big" @click="removeTheme">Удалить</button>
            </div>
        </div>
    </div>

    <div v-if="isModalDeleteTaskInfoOpen" class="overlay">
        <div class="delete-confirm">
            <div class="text-modal">
                <span>Вы <b>уверены</b>, что хотите</span>
                <span><span class="red-font">удалить</span> выбранную задачу?</span>
            </div>
            <div class="btns-container">
                <button class="cancel-btn" @click="closeDeleteTaskModal">Вернуться</button>
                <button class="delete-btn-big" @click="removeTask">Удалить</button>
            </div>
        </div>
    </div>

    <button class="add-theme-btn" @click="toCreateTheme">Добавить тему</button>

    <div v-if="isModalFormTaskOpen" class="overlay">
        <formTheme class="task-edit-formTheme" @submit.prevent="handleSubmit" novalidate>
            
            <h1>Задача</h1>
            

            <div class="field">
                <span>Изображение</span>
                <input 
                id="image"
                v-model="formTask.image"
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
                v-model="formTask.title"
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
                <span>Условие задачи</span>
                <textarea
                id="description"
                v-model="formTask.description"
                type="text" 
                class="box"
                placeholder="Текст задачи.."
                aria-errormessage="description-errors"
                title=""></textarea>
                <span class="field__errors" id="description-errors"></span>
            </div>

            <div class="field">
                <span>Количество попыток</span>
                <input
                id="attemps"
                v-model="formTask.attemps"
                type="text" 
                class="box"
                placeholder="Введите число.."
                aria-errormessage="attemps-errors"
                title="">
                <span class="field__errors" id="attemps-errors"></span>
            </div>

            <div class="field">
                <span>Минимальная оценка для выполнения</span>
                <input
                id="minMark"
                v-model="formTask.minMark"
                type="text" 
                class="box"
                placeholder="Введите число.."
                aria-errormessage="minMark-errors"
                title="">
                <span class="field__errors" id="minMark-errors"></span>
            </div>

            <div class="field">
                <span>Максимальная оценка</span>
                <input
                id="maxMark"
                v-model="formTask.maxMark"
                type="text" 
                class="box"
                placeholder="Введите число.."
                aria-errormessage="maxMark-errors"
                title="">
                <span class="field__errors" id="maxMark-errors"></span>
            </div>

            <div class="btns-container">
                <button class="cancel-btn" @click="closeFormTask">Отменить</button>
                <button class="save-btn" @click="saveTask">Сохранить</button>
            </div>
        </formTheme>
    </div>

</template>

<script>
    import { reactive, ref } from 'vue';
    import { useRoute, useRouter } from 'vue-router';
    import { URL_IMG_THEME_CARD_DEFAULT } from '@/constants'
    import { logResultIfFailure, getRole } from '@/utils/shared/shared';

    import { getCourseTitleAndTeacher } from '@/utils/requests/courses';
    import { getAllThemes, getTheme, updateTheme, createTheme, deleteTheme } from '@/utils/requests/themes';
    import { getTasksByTheme, createTask, deleteTask } from '@/utils/requests/tasks';
 
    export default {
            name: "ThemesPage",
        setup() {
            const courseTitle = ref('');
            const themes = ref([]);
            const route = useRoute();
            const router = useRouter();
            const themeImage = URL_IMG_THEME_CARD_DEFAULT;

            let isModalFormOpen = ref(false);
            let isModalDeleteInfoOpen = ref(false);
            let isModalFormTaskOpen = ref(false);
            let isModalDeleteTaskInfoOpen = ref(false);

            let haveNoRightsModal = ref(false);

            function openNoRightsModal(){
                haveNoRightsModal.value = true;
            }

            function closeNoRightsModal(){
                haveNoRightsModal.value = false;
            }

            const formTheme = reactive({
                themeId: null,
                image: null,
                title: null,
                description: null,
                method: null
            });
            const formTask = reactive({
                themeId: null,
                taskId: null,
                image: null,
                title: null,
                description: null,
                attemps: null,
                minMark: null,
                maxMark: null
            })

            return { haveNoRightsModal, openNoRightsModal, closeNoRightsModal, courseTitle, themes, route, router, themeImage, isModalFormOpen, isModalDeleteInfoOpen, isModalFormTaskOpen, isModalDeleteTaskInfoOpen, formTheme, formTask};
        },

        async mounted() {

            const courseId = this.$route.query.id;
                if (courseId) {

                    const courseTitleAndTeacherResult = await getCourseTitleAndTeacher(courseId);

                    if (logResultIfFailure(courseTitleAndTeacherResult)){
                        return;
                    }

                    this.courseTitle = courseTitleAndTeacherResult.title;

                    await this.getThemes(courseId);
                } 
                else {
                    console.error('No course ID provided');
            }
        },

        methods: {

            async addTasksToThemes(themes) {

                let result = [];

                for (let i=0; i<themes.length; i++) {

                    const tasksResult = await getTasksByTheme(themes[i].id);

                    if (logResultIfFailure(tasksResult)) {
                        return;
                    }

                    const theme = {
                        ...themes[i],
                        tasks: tasksResult
                    }

                    result.push(theme);
                }

                return result;
            },

            async getThemes(courseId) {

                const themesResult = await getAllThemes(courseId);

                if (logResultIfFailure(themesResult)) {
                    return;
                }
                
                const themes = await this.addTasksToThemes(themesResult);
                this.themes = themes.sort((a, b) => a.number - b.number);
            },

            goToTask(taskId) {
                this.router.push({ 
                    name: 'taskPage', 
                    query: { id: taskId } 
                });
            },

            toCreateTheme() {

                if (getRole() != "Teacher" && getRole() != "Admin"){
                    this.openNoRightsModal();
                    return;
                }

                this.formTheme.image = URL_IMG_THEME_CARD_DEFAULT;
                this.formTheme.title = null;
                this.formTheme.description = null;

                this.isModalFormOpen = true;
            },

            async saveTheme(){

                const request = {
                    photo: this.formTheme.image,
                    title: this.formTheme.title,
                    text: this.formTheme.description
                }

                const themeId = this.formTheme.themeId;

                if (themeId == null) {

                    const result = await createTheme(request);
                    
                    if (logResultIfFailure(result)) {
                        return;
                    }

                    console.log("Тема успешно создана:", result);

                    request.id = result;

                    this.themes.push(request);
                }
                else {
                    const result = await updateTheme(themeId, request);

                    if (logResultIfFailure(result)) {
                        return;
                    }

                    console.log("Тема успешно обновлена:", result);

                    await this.refresh(request);
                }

                this.closeForm();
            },

            async refresh() {

                const themeId = this.formTheme.themeId;
                const themeResult = await getTheme(themeId);

                if (logResultIfFailure(themeResult)) {
                    return;
                }

                const updatedTheme = {
                    "id": themeId,
                    "title": themeResult.title,
                    "image": themeResult.photo,
                    "text": themeResult.text
                }

                this.themes = this.themes.map(theme =>
                    theme.id === this.formTheme.themeId ? { ...theme, ...updatedTheme } : theme);
            },

            closeForm() {
                this.formTheme.themeId = null;
                this.isModalFormOpen = false;
            },

            closeFormTask() {
                this.formTask.themeId = null;
                this.isModalFormTaskOpen = false;

                this.formTask.title = null;
                this.formTask.description = null;
                this.formTask.attemps = null;
                this.formTask.minMark = null;
                this.formTask.maxMark = null;
            },

            async toEditTheme(themeId) {

                if (getRole() != "Teacher" && getRole() != "admin"){
                    this.openNoRightsModal();
                    return;
                }

                this.formTheme.themeId = themeId;
                const result = await getTheme(themeId);

                if (typeof result === "object" && "errors" in result) {
                    console.log("Have Errors", result.errors);
                    return
                }

                this.formTheme.title = result.title;
                this.formTheme.image = result.photo;
                this.formTheme.description = result.text;

                this.isModalFormOpen = true;
            },

            toDeleteTheme(themeId) {

                if (getRole() != "Teacher" && getRole() != "Admin"){
                    this.openNoRightsModal();
                    return;
                }

                this.formTheme.themeId = themeId;
                this.isModalDeleteInfoOpen = true;
            },

            closeDeleteModal() {
                this.formTheme.themeId = null;
                this.isModalDeleteInfoOpen = false;
            },

            async removeTheme() {

                const result = await deleteTheme(this.formTheme.themeId);

                if (logResultIfFailure(result)) {
                    return;
                }

                console.log("Тема успешно удалена:", result);

                this.themes = this.themes.filter(theme => theme.id !== this.formTheme.themeId);

                this.closeDeleteModal()
            },

            toCreateTask(themeId) {

                if (getRole() != "Teacher" && getRole() != "admin"){
                    this.openNoRightsModal();
                    return;
                }

                this.isModalFormTaskOpen = true;
                this.formTask.themeId = themeId;
            },

            async saveTask(){

                const request = {
                    themeId: this.formTask.themeId,
                    title: this.formTask.title,
                    condition: this.formTask.description,
                    attempsAmount: this.formTask.attemps,
                    minMark: this.formTask.minMark,
                    maxMark: this.formTask.maxMark
                }

                const result = await createTask(request);

                if (logResultIfFailure(result)) {
                    return;
                }

                console.log("Задача успешно создана:", result);

                this.themes = this.themes.map(theme =>
                    theme.id === this.formTask.themeId ? { ...theme, tasks: [...theme.tasks, request] } : theme
                );
                
                this.closeFormTask();
            },

            toDeleteTask(event, taskId) {

                event.stopPropagation(event);

                if (getRole() != "Teacher" && getRole() != "admin"){
                    this.openNoRightsModal();
                    return;
                }

                this.formTask.taskId = taskId;
                this.isModalDeleteTaskInfoOpen = true;
            },

            async removeTask() {

                const taskId = this.formTask.taskId;

                const result = await deleteTask(taskId);

                if (logResultIfFailure(result)) {
                    return;
                }

                console.log("Задача успешно удалена:", result);

                this.themes = this.themes.map(theme => ({
                    ...theme,
                    tasks: theme.tasks.filter(task => task.id !== taskId)
                }));

                this.closeDeleteTaskModal()
            },

            closeDeleteTaskModal(){
                this.formTask.taskId = null;
                this.isModalDeleteTaskInfoOpen = false;
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

    .container .tasks{
        width: 100%;

        display: flex;
        flex-direction: column;

        align-items: flex-start;
    }

    .container .tasks .task{
        margin-top: 10px;
        margin-left: 20%;
        width: auto;
        color: #fff;
        cursor: pointer;

        display: flex;
        flex-direction: row;
        flex-wrap: nowrap;

        align-items: center;
        justify-content: flex-start;

        gap: 10px;
    }

    .container .task .delete-btn{
        margin-top: 0;
        margin-left: 20%;
        position: absolute;
    }

    .container .task span{
        font-size: 20px;
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

    .theme-title {
        width: 100%;
        display: flex;
        flex-direction: row;
        justify-content: center;
    }

    .theme-btns-container {
        margin-left: 30px;
    }

    .add-task-btn {
        margin-left: 30%;
    }

</style>