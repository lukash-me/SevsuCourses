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
    <div></div>
    <div class="container">
        <div class="course-add-card">
            <div class="add-button" @click="openFormModal">Добавить курс
                <img src="images/add.jpg" alt="">
            </div>
        </div>
        <div v-for="(cardsRow, index) in courseRows" :key="index" class="cards-row">
            <div
                v-for="(course) in cardsRow"
                :key="course.id"
                class="course-card"
                :data-id="course.id"
                @click="goToCourse(course.id)"
                >
                <div class="image">
                    <img :src="course.photo" alt="Course Image"/>
                    <div class="edit-btn" @click="editCourse($event, course.id)">Редактировать</div>
                    <div class="delete-btn" @click="toDeleteCourse($event, course.id)">Удалить</div>
                </div>
                <div class="card-text">
                    <h1>{{ course.title }}</h1>
                    <span>{{ course.description }}</span>
                </div>
            </div>
        </div>
    </div>

    <div v-if="isModalFormOpen" class="overlay">
        <form class="course-edit-form" @submit.prevent="handleSubmit" novalidate>
            
            <h1>Курс</h1>
            

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
                <span>Описание</span>
                <textarea
                id="description"
                v-model="form.description"
                type="text" 
                class="box"
                placeholder="Краткое описание того, что ждет студента на курсе.."
                aria-errormessage="description-errors"
                title=""></textarea>
                <span class="field__errors" id="description-errors"></span>
            </div>

            <div class="btns-container">
                <button class="cancel-btn" @click="closeFormModal">Отменить</button>
                <button class="save-btn" @click="saveCourse">Сохранить</button>
            </div>
        </form>
    </div>

    <div v-if="isModalDeleteInfoOpen" class="overlay">
        <div class="delete-confirm">
            <div class="text-modal">
                <span>Вы <b>уверены</b>, что хотите</span>
                <span><span class="red-font">удалить</span> выбранный курс?</span>
            </div>
            <div class="btns-container">
                <button class="cancel-btn" @click="closeDeleteModal">Вернуться</button>
                <button class="delete-btn-big" @click="removeCourse">Удалить</button>
            </div>
        </div>
    </div>

    
</template>

<script>
import { useRouter } from 'vue-router';
import { reactive, ref } from 'vue';
import { logResultIfFailure, getRole, getId } from '@/utils/shared/shared'
import { getCourseInfo, createCourse, updateCourseInfo, getAllCourses } from '@/utils/requests/courses'

export default {
    name: 'CoursesPage',

    setup() {
        const router = useRouter();
        const courseImage = '/images/add.jpg';
        let isModalFormOpen = ref(false);
        let isModalDeleteInfoOpen = ref(false); 
        const form = reactive({
            courseId: null,
            image: null,
            title: null,
            description: null,
            method: null
        });

        let haveNoRightsModal = ref(false);

        function openNoRightsModal(){
            haveNoRightsModal.value = true;
        }

        function closeNoRightsModal(){
            haveNoRightsModal.value = false;
        }

        return { router, 
            courseImage, 
            isModalFormOpen, 
            isModalDeleteInfoOpen, 
            form,
            haveNoRightsModal,
            openNoRightsModal,
            closeNoRightsModal};
    },

    data() {
        return {
            courses: [],
            courseRows: [],
        };
    },

    async mounted() {
        
        await this.showAllCourses();
    },

    methods: {

        async showAllCourses() {

            const coursesResult = await getAllCourses();
            if (logResultIfFailure(coursesResult)) {
                return;
            }

            let inRow = 1;
            let cardsRow = [];
            
            coursesResult.forEach((course) => {
                if (inRow === 4) {
                    this.courseRows.push(cardsRow);
                    cardsRow = [];
                    inRow = 0;
                }
                this.courseImage = course.photo;
                cardsRow.push(course);
                inRow++;
            });
            if (cardsRow.length > 0) {
                this.courseRows.push(cardsRow);
            }
        },

        goToCourse(courseId) {
            this.router.push({ 
                name: 'themesPage',
                query: {id: courseId } 
            });
        },

        async editCourse(event, courseId) {
            event.stopPropagation(event);

            if (getRole() != "Teacher" && getRole() != "Admin"){
                this.openNoRightsModal();
                return;
            }

            const courseInfoResult = await getCourseInfo(courseId);

            if (logResultIfFailure(courseInfoResult)) {
                return;
            }

            this.form.courseId = courseId;
            this.form.image = courseInfoResult.photo;
            this.form.title = courseInfoResult.title;
            this.form.description = courseInfoResult.description;
            this.form.method = "PUT";

            this.openFormModal();
        },

        async saveCourse() {
            
            if (this.form.method === "PUT") {

                const request = {
                    photo: this.form.image,
                    title: this.form.title,
                    description: this.form.description
                }

                const courseId = this.form.courseId;

                const resultCourseInfo = await updateCourseInfo(courseId, request);

                if (logResultIfFailure(resultCourseInfo)) {
                    return;
                }
                
                console.log("Курс успешно изменен", resultCourseInfo);

                this.closeFormModal();
                await this.refreshCard();
            }
            else {

                const request = {
                    teacherId: getId(),
                    photo: this.form.image,
                    title: this.form.title,
                    description: this.form.description
                }

                const result = await createCourse(request);

                if (logResultIfFailure(result)) {
                    return;
                }

                console.log("Курс успешно добавлен:", result);

                const course = {
                    id: result,
                    image: request.photo,
                    title: request.title,
                    description: request.description
                };

                this.closeFormModal();
                this.addCard(course);
            }
        },

        async refreshCard() {

            const courseId = this.form.courseId;
            const courseInfoResult = await getCourseInfo(courseId);

            if (logResultIfFailure(courseInfoResult)) {
                return;
            }
            
            const updatedCourse = {
                "id": courseId,
                "title": courseInfoResult.title,
                "image": courseInfoResult.photo,
                "description": courseInfoResult.description
            } 

            this.courseRows = this.courseRows.map(row =>
                row.map(course => 
                    (course.id === courseId ? { ...course, ...updatedCourse } : course))
            );
        },

        // Отображение новой карточки на странице
        async addCard(course) {

            if (this.courseRows[this.courseRows.length - 1].length != 4) {                
                this.courseRows[this.courseRows.length - 1].push(course);
            }
            else {
                this.courseRows.push([course]);
            }
        },

        toDeleteCourse(event, id) {

            event.stopPropagation();

            if (getRole() != "Teacher" && getRole() != "admin"){
                this.openNoRightsModal();
                return;
            }

            this.form.courseId = id;

            this.openDeleteModal(event);
        },

        async removeCourse() {

            const courseId = this.form.courseId;
            const result = await this.deleteCourse(courseId);

            if (logResultIfFailure(result)) {
                return;
            }

            console.log("Курс успешно удален:", result);

            this.closeDeleteModal();

            this.courseRows = this.courseRows.map(row =>
                row.filter(course => course.id !== courseId)
            );
        },

        openFormModal() {

            if (getRole() != "Teacher" && getRole() != "Admin"){
                this.openNoRightsModal();
                return;
            }

            this.isModalFormOpen = true;
        },

        closeFormModal() {

            this.form.courseId = null;
            this.form.image = null;
            this.form.title = null;
            this.form.description = null;

            this.isModalFormOpen = false;
        },

        openDeleteModal() {

            this.isModalDeleteInfoOpen = true;
        },

        closeDeleteModal() {
            this.isModalDeleteInfoOpen = false;
        }
    },
};


</script>

<style scoped>

.container {
    display: flex;
    
    width: 90%;
    gap: 10px;
    flex-wrap: wrap;

    margin: 0 auto;
    margin-top: 10rem;
}

.btns-container {
        margin-top: 15px;
        display: flex;
        gap: 35px;
    }

.add-button {
    background: #fff;
}


</style>