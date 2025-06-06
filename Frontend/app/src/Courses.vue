<template>

    <NoRightsModal v-if="isNoRightsModalOpen" 
        @close="() => isNoRightsModalOpen=false"
    />

    <DeleteCourseConfirm v-if="isDeleteConfirmOpen"
        @close="() => isDeleteConfirmOpen=false"
        @delete="removeCourse"
    />

    <FormCourse v-if="isCourseModalOpen"
        :form="courseForm"
        @close="() => isCourseModalOpen=false"
        @submit="doCourseCreateOrUpdate"
    />

    <div class="wrapper">
        <div class="container">
            <div class="course-add-card" v-if="router.currentRoute.value.query.page == 0">
                <div class="add-button" @click="openCourseForm($event, null)">Добавить курс
                    <img :src="URL_IMG_COURSE_ADD_CARD_DEFAULT" alt="">
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
                        <img :src="course.photo" 
                            alt="Course Image"
                            @error="event => event.target.src = URL_IMG_COURSE_CARD_DEFAULT"
                        />
                        <div class="edit-btn" @click="openCourseForm($event, course.id)">Редактировать</div>
                        <div class="delete-btn" @click="deleteCourse($event, course.id)">Удалить</div>
                    </div>
                    <div class="card-text">
                        <h1>{{ course.title }}</h1>
                        <span>{{ course.description }}</span>
                    </div>
                </div>
            </div>
        </div>
        <div class="page-selector">
            <table>
                <tbody>
                    <tr>
                        <td v-for="i in pagesAmount" :key="i" @click="changePage(i - 1)">
                            {{ i }}
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <footer></footer>
    </div>
    
    

</template>

<script>
import { useRouter } from 'vue-router';
import { ref } from 'vue';
import { getRole, getId } from '@/utils/shared/shared'
import { getCourseInfo, createCourse, updateCourseInfo, deleteCourse, getCoursesOnPage } from '@/utils/requests/courses'
import { URL_IMG_COURSE_ADD_CARD_DEFAULT, URL_IMG_COURSE_CARD_DEFAULT } from '@/constants'

import NoRightsModal from '@/components/NoRightsModal.vue';
import DeleteCourseConfirm from '@/components/DeleteCourseConfirm.vue';
import FormCourse from '@/components/FormCourse.vue';

//import { openModal, closeModal } from '@/utils/modals/modals';

export default {
    name: 'CoursesPage',

    components: {
        NoRightsModal,
        DeleteCourseConfirm,
        FormCourse
    },

    setup() {
        const router = useRouter();
        const isNoRightsModalOpen = ref(false);
        const isCourseModalOpen = ref(false);
        const isDeleteConfirmOpen = ref(false);

        const courseForm = ref({
            courseId: null,
            photo: null,
            title: null,
            description: null
        });

        return { 
            router, 
            URL_IMG_COURSE_CARD_DEFAULT,
            URL_IMG_COURSE_ADD_CARD_DEFAULT,
            isCourseModalOpen, 
            isDeleteConfirmOpen, 
            courseForm,
            isNoRightsModalOpen,
        };
    },

    data() {
        return {
            courseRows: [],
            pagesAmount: 1
        };
    },

    async mounted() {
        
        await this.showAllCourses();
        console.log(this.courseRows)
    },

    methods: {

        async showAllCourses() {
            this.courseRows = [];

            const maxCardsInRow = 4;
            const maxRows = 2;
            let cardsRow = [];

            const pageNumber = this.router.currentRoute.value.query.page;

            const coursesResult = await getCoursesOnPage(pageNumber);

            // Todo Проверка

            const courses = coursesResult.courses;
            
            this.pagesAmount = coursesResult.pagesAmount;

            let inRow = 0;

            if (pageNumber == 0) {
                inRow = 1;
            }

            for(let i=0; i<courses.length; i++) {
                if (inRow === maxCardsInRow) {
                    this.courseRows.push(cardsRow);
                    cardsRow = [];
                    inRow = 0;
                    if (this.courseRows.length==maxRows) {
                        return;
                    }
                }
                cardsRow.push(courses[i]);
                inRow++;
            }
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

        changePage(pageNumber) {
            window.location.href = this.router.resolve({ name: 'coursesPage', query: { page: pageNumber } }).href;
        },

        async createCourse() {

            const request = {
                teacherId: getId(),
                photo: this.courseForm.photo,
                title: this.courseForm.title,
                description: this.courseForm.description
            }

            const result = await createCourse(request);

            console.log("Курс успешно добавлен:", result);

            this.isCourseModalOpen = false;
            this.showAllCourses();
        },

        updateCourseForm(form) {

            this.courseForm.photo = form.photo;
            this.courseForm.title = form.title;
            this.courseForm.description = form.description;
        },

        async fillCourseForm(courseId) {

            const courseInfoResult = await getCourseInfo(courseId);

            this.courseForm.courseId = courseId;
            this.courseForm.photo = courseInfoResult.photo;
            this.courseForm.title = courseInfoResult.title;
            this.courseForm.description = courseInfoResult.description;
        },

        async openCourseForm(event, courseId) {
            event.stopPropagation();

            if (getRole() != "Teacher" && getRole() != "admin"){
                this.isNoRightsModalOpen = true;
                return;
            }

            this.courseForm.id = courseId;

            if (courseId) {
                await this.fillCourseForm(courseId);
            }
            
            this.isCourseModalOpen = true;
        },

        async doCourseCreateOrUpdate(form) {

            if (form instanceof Event) {
                return;
            }

            this.updateCourseForm(form);

            if (this.courseForm.id) {
                await this.updateCourse();
                this.clearCourseForm();
                return;
            }

            await this.createCourse();
            this.clearCourseForm();
            return;
        },

        async updateCourse() {

            const request = {
                photo: this.courseForm.photo,
                title: this.courseForm.title,
                description: this.courseForm.description
            }

            const courseId = this.courseForm.courseId;
            const resultCourseInfo = await updateCourseInfo(courseId, request);
            
            console.log("Курс успешно изменен", resultCourseInfo);

            this.isCourseModalOpen = false;
            await this.refreshCard(request);
        },

        async refreshCard(request) {

            const courseId = this.courseForm.courseId;
            
            const updatedCourse = {
                "id": courseId,
                "title": request.title,
                "image": request.photo,
                "description": request.description
            }

            this.courseRows = this.courseRows.map(row =>
                row.map(course => 
                    (course.id === courseId ? { ...course, ...updatedCourse } : course))
            );
        },

        deleteCourse(event, id) {

            event.stopPropagation();

            if (getRole() != "Teacher" && getRole() != "admin"){
                this.isNoRightsModalOpen = true;
                return;
            }

            this.courseForm.courseId = id;
            this.isDeleteConfirmOpen = true;
        },

        async removeCourse() {

            const courseId = this.courseForm.courseId;
            const result = await deleteCourse(courseId);

            console.log("Курс успешно удален:", result);

            this.clearCourseForm();

            this.isDeleteConfirmOpen = false;

            this.showAllCourses();
        },

        clearCourseForm() {

            this.courseForm.courseId = null;
            this.courseForm.photo = null;
            this.courseForm.title = null;
            this.courseForm.description = null;
        },
    },
};


</script>

<style scoped>

.add-button {
    background: #fff;
}

.page-selector {
    margin-top: 32px;
    margin-bottom: 40px;
    display: flex;
    justify-content: center;
}

.page-selector td {
    font-size: 26px;
    padding-left: 6px;
    padding-right: 6px;

    cursor: pointer;

    transition: .5s;
}

.page-selector td:hover {
    color: rgb(114, 222, 255);
}

.container {
    min-height: 83vh;
    display: flex;
    justify-content: center;

    flex: 1;
}

footer {
    height: 200px;
    background-color: #0b1421;
}

.wrapper {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
}


</style>