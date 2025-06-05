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

    <div class="container">
        <div class="course-add-card">
            <div class="add-button" @click="openCourseForm($event, null)">Добавить курс
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

</template>

<script>
import { useRouter } from 'vue-router';
import { ref } from 'vue';
import { getRole, getId } from '@/utils/shared/shared'
import { getCourseInfo, createCourse, updateCourseInfo, getAllCourses, deleteCourse } from '@/utils/requests/courses'
import { URL_IMG_COURSE_ADD_CARD_DEFAULT } from '@/constants'

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
        const courseImage = URL_IMG_COURSE_ADD_CARD_DEFAULT;
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
            courseImage, 
            isCourseModalOpen, 
            isDeleteConfirmOpen, 
            courseForm,
            isNoRightsModalOpen,
        };
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

        async createCourse() {

            const request = {
                teacherId: getId(),
                photo: this.courseForm.photo,
                title: this.courseForm.title,
                description: this.courseForm.description
            }

            const result = await createCourse(request);

            console.log("Курс успешно добавлен:", result);

            const course = {
                id: result,
                image: request.photo,
                title: request.title,
                description: request.description
            };

            this.isCourseModalOpen = false;
            this.addCard(course);
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

        // Отображение новой карточки на странице
        async addCard(course) {

            if (this.courseRows[this.courseRows.length - 1].length != 4) {                
                this.courseRows[this.courseRows.length - 1].push(course);
            }
            else {
                this.courseRows.push([course]);
            }
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

            this.isDeleteConfirmOpen = false;

            this.courseRows = this.courseRows.map(row =>
                row.filter(course => course.id !== courseId)
            );

            this.clearCourseForm();
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


</style>