<template>

    <FormTheme v-if="isThemeModalOpen"
        :form="themeForm"
        @close="() => isThemeModalOpen=false"
        @submit="doThemeCreateOrUpdate"
    />

    <FormTask v-if="isTaskModalOpen"
        :form="taskForm"
        @close="() => isTaskModalOpen=false"
        @submit="createTask"
    />

    <NoRightsModal v-if="isNoRightsModalOpen"
        @close="() => isNoRightsModalOpen=false"/>

    <DeleteThemeConfirm v-if="isDeleteThemeConfirmOpen"
        @close="() => isDeleteThemeConfirmOpen=false"
        @delete="removeTheme"
    />

    <DeleteTaskConfirm v-if="isDeleteTaskConfirmOpen"
        @close="() => isDeleteTaskConfirmOpen=false"
        @delete="removeTask"
    />

    <div v-for="theme in themes" :key="theme.id" class="container">
        <div class="theme-title">
            <h2>{{ theme.title }}</h2>
            <div class="theme-btns-container">
                <div class="edit-btn" @click="openThemeForm(theme.id)">Редактировать</div>
                <div class="delete-btn" @click="deleteTheme(theme.id)">Удалить</div>
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
          <button class="delete-btn" @click="deleteTask($event, task.id)">Удалить</button>
        </div>
            <button class="add-task-btn" @click="openTaskForm(theme.id)">Добавить задачу</button>
        </div>
    </div>

    <button class="add-theme-btn" @click="openThemeForm(null)">Добавить тему</button>

</template>

<script>
    import { ref } from 'vue';
    import { useRoute, useRouter } from 'vue-router';
    import { URL_IMG_THEME_CARD_DEFAULT } from '@/constants'
    import { getRole } from '@/utils/shared/shared';

    import { getCourseTitleAndTeacher } from '@/utils/requests/courses';
    import { getAllThemes, getTheme, updateTheme, createTheme, deleteTheme } from '@/utils/requests/themes';
    import { getTasksByTheme, createTask, deleteTask, getTask } from '@/utils/requests/tasks';

    import FormTheme from '@/components/FormTheme.vue';
    import NoRightsModal from '@/components/NoRightsModal.vue';
    import DeleteThemeConfirm from './components/DeleteThemeConfirm.vue';
    import DeleteTaskConfirm from './components/DeleteTaskConfirm.vue';
    import FormTask from './components/FormTask.vue';
 
    export default {

        name: "ThemesPage",

        components: {
            FormTheme,
            NoRightsModal,
            DeleteThemeConfirm,
            DeleteTaskConfirm,
            FormTask
        },

        setup() {
            const courseTitle = ref('');
            const themes = ref([]);
            const route = useRoute();
            const router = useRouter();
            const themeImage = URL_IMG_THEME_CARD_DEFAULT;

            const isThemeModalOpen = ref(false);
            const isTaskModalOpen = ref(false);

            const isDeleteThemeConfirmOpen = ref(false);
            const isDeleteTaskConfirmOpen = ref(false);

            const isNoRightsModalOpen = ref(false);

            const themeForm = ref({
                id: null,
                photo: null,
                title: null,
                description: null,
            });

            const taskForm = ref({
                id: null,
                themeId: null,
                photo: null,
                title: null,
                condition: null,
                minMark: null,
                maxMark: null
            });

            return { 
                isNoRightsModalOpen, 
                courseTitle, 
                themes, 
                route, 
                router, 
                themeImage, 
                isThemeModalOpen,
                isTaskModalOpen,
                themeForm, 
                taskForm,
                isDeleteThemeConfirmOpen,
                isDeleteTaskConfirmOpen,
            };
        },

        async mounted() {

            const courseId = this.$route.query.id;

                if (courseId) {

                    const courseTitleAndTeacherResult = await getCourseTitleAndTeacher(courseId);

                    this.courseTitle = courseTitleAndTeacherResult.title;

                    await this.getThemes(courseId);
                } 
                else {
                    console.error('No course ID provided');
            }
        },

        methods: {

            // Themes

            async addTasksToThemes(themes) {

                let result = [];

                for (let i=0; i<themes.length; i++) {

                    const tasksResult = await getTasksByTheme(themes[i].id);

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
                
                const themes = await this.addTasksToThemes(themesResult);
                this.themes = themes.sort((a, b) => a.number - b.number);
            },

            async fillThemeForm() {

                const themeId = this.themeForm.id;
                const themeResult = await getTheme(themeId);

                this.themeForm.photo = themeResult.photo;
                this.themeForm.title = themeResult.title;
                this.themeForm.description = themeResult.text;
            },

            updateThemeForm(form) {

                this.themeForm.photo = form.photo;
                this.themeForm.title = form.title;
                this.themeForm.description = form.description;
            },

            clearThemeForm() {
                this.themeForm.id = null;
                this.themeForm.photo = null;
                this.themeForm.title = null;
                this.themeForm.description = null;
            },

            async openThemeForm(themeId) {
                this.clearThemeForm();

                if (getRole() != "Teacher" && getRole() != "admin"){
                    this.isNoRightsModalOpen = true;
                    return;
                }
                
                if (themeId) {
                    this.themeForm.id = themeId;
                    await this.fillThemeForm();
                }

                this.isThemeModalOpen = true;
            },

            async doThemeCreateOrUpdate(form) {
                if (form instanceof Event) {
                    return;
                }

                this.updateThemeForm(form);

                if (this.themeForm.id) {
                    await this.updateTheme();
                    return;
                }

                await this.createTheme();
                return;
            },

            async createTheme() {

                // Условие, что фото не найдено
                this.themeForm.photo = URL_IMG_THEME_CARD_DEFAULT;

                const request = {
                    photo: this.themeForm.photo,
                    title: this.themeForm.title,
                    text: this.themeForm.description
                }

                const courseId = this.route.query.id;
                const result = await createTheme(courseId, request);

                console.log("Тема успешно создана:", result);

                request.id = result;
                this.themes.push(request);

                this.isThemeModalOpen = false;
            },

            async updateTheme(){

                const request = {
                    photo: this.themeForm.photo,
                    title: this.themeForm.title,
                    text: this.themeForm.description
                }

                const themeId = this.themeForm.id;
                const result = await updateTheme(themeId, request);

                console.log("Тема успешно обновлена:", result);

                await this.refresh(request);
                
                this.isThemeModalOpen = false;
            },

            deleteTheme(themeId) {
                this.clearThemeForm();

                if (getRole() != "Teacher" && getRole() != "admin"){
                    this.isNoRightsModalOpen = true;
                    return;
                }

                this.themeForm.id = themeId;
                this.isDeleteThemeConfirmOpen = true;
            },

            async removeTheme() {

                const result = await deleteTheme(this.themeForm.id);

                console.log("Тема успешно удалена:", result);

                this.themes = this.themes.filter(theme => theme.id !== this.themeForm.id);

                this.isDeleteThemeConfirmOpen = false;
            },

            async refresh() {

                const themeId = this.themeForm.id;
                const themeResult = await getTheme(themeId);

                const updatedTheme = {
                    "id": themeId,
                    "title": themeResult.title,
                    "image": themeResult.photo,
                    "text": themeResult.text
                }

                this.themes = this.themes.map(theme =>
                    theme.id === this.themeForm.id ? { ...theme, ...updatedTheme } : theme);
            },

            // Tasks

            goToTask(taskId) {
                this.router.push({ 
                    name: 'taskPage', 
                    query: { id: taskId } 
                });
            },

            async fillTaskForm() {

                const taskId = this.taskForm.id;
                const tasksResult = await getTask(taskId);

                this.taskForm.themeId = tasksResult.themeId;
                this.taskForm.photo = tasksResult.photo;
                this.taskForm.title = tasksResult.title;
                this.taskForm.condition = tasksResult.condition;
                this.taskForm.minMark = tasksResult.minMark;
                this.taskForm.maxMark = tasksResult.maxMark;
            },

            updateTaskForm(form) {

                this.taskForm.photo = form.photo;
                this.taskForm.title = form.title;
                this.taskForm.condition = form.condition;
                this.taskForm.minMark = form.minMark;
                this.taskForm.maxMark = form.maxMark;
            },

            clearTaskForm() {
                this.taskForm.id = null;
                this.taskForm.themeId = null;
                this.taskForm.photo = null;
                this.taskForm.title = null;
                this.taskForm.condition = null;
                this.taskForm.minMark = null;
                this.taskForm.maxMark = null;
            },

            async openTaskForm(themeId) {
                this.clearTaskForm();

                if (getRole() != "Teacher" && getRole() != "admin"){
                    this.isNoRightsModalOpen = true;
                    return;
                }

                this.taskForm.themeId = themeId;
                this.isTaskModalOpen = true;
            },

            async createTask(form) {
                if (form instanceof Event) {
                    return;
                }
                
                this.updateTaskForm(form);
                this.taskForm.photo = URL_IMG_THEME_CARD_DEFAULT;

                const request = {
                    themeId: this.taskForm.themeId,
                    photo: this.taskForm.photo,
                    title: this.taskForm.title,
                    condition: this.taskForm.condition,
                    minMark: this.taskForm.minMark,
                    maxMark: this.taskForm.maxMark,
                    attempsAmount: this.taskForm.attemps
                }

                const result = await createTask(request);

                console.log("Задача успешно создана:", result);

                const taskMainInfo = {
                    id: result,
                    title: request.title
                }

                this.themes = this.themes.map(theme =>
                    theme.id === this.taskForm.themeId ? { ...theme, tasks: [...(theme.tasks || []), taskMainInfo] } : theme
                );

                this.isTaskModalOpen = false;
            },

            deleteTask(event, taskId) {
                event.stopPropagation(event);

                this.clearTaskForm();

                if (getRole() != "Teacher" && getRole() != "admin"){
                    this.isNoRightsModalOpen = true;
                    return;
                }

                this.taskForm.id = taskId;
                this.isDeleteTaskConfirmOpen = true;
            },

            async removeTask() {

                const taskId = this.taskForm.id;
                const result = await deleteTask(taskId);

                console.log("Задача успешно удалена:", result);

                this.themes = this.themes.map(theme => ({
                    ...theme,
                    tasks: theme.tasks.filter(task => task.id !== taskId)
                }));

                this.isDeleteTaskConfirmOpen = false;
            },
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
        min-height: 900px;
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