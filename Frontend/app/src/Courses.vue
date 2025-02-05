<template>
    <div class="container">
        <div v-for="(cardsRow, index) in courseRows" :key="index" class="cards-row">
            <div
                v-for="(course) in cardsRow"
                :key="course.id"
                class="course-card"
                :data-id="course.id"
                @click="goToCourse(course.id)"
                >
                <div class="image">
                    <img :src="courseImage" alt="Course Image"/>
                    <div class="edit-btn" @click="editCourse($event, course.id)">Редактировать</div>
                    <div class="delete-btn" @click="deleteCourse($event, course.id)">Удалить</div>
                </div>
                <div class="card-text">
                    <h1>{{ course.title }}</h1>
                    <span>{{ course.description }}</span>
                </div>
            </div>
            <div class="course-add-card">
                <div class="add-button" @click="createCourse">Добавить курс</div>
            </div>
        </div>
    </div>

    <div v-if="isModalOpen" class="overlay">
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
                <button class="cancel-btn" @click="closeModal">Отменить</button>
                <button class="save-btn" @click="updateCourse">Сохранить</button>
            </div>
        </form>
    </div>
</template>

<script>
import { useRouter } from 'vue-router';
import { reactive, ref } from 'vue';

export default {
    name: 'CoursesPage',

    setup() {
        const router = useRouter();
        const courseImage = '/images/abstract_02.jpg';
        let isModalOpen = ref(false); 
        const form = reactive({
            courseId: null,
            image: null,
            title: null,
            description: null
        });

        return { router, courseImage, isModalOpen, form };
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
        async getAllCourses() {
            try {
                const response = await fetch('http://localhost:5036/Course');
                if (!response.ok) {
                    throw new Error('Network response was not ok ' + response.statusText);
                }
                const data = await response.json();
                return data;
            } 
            catch (error) {
                console.error('There was a problem with the fetch operation:', error);
            }
        },

        async getCourseInfo(courseId) {
            try {
                const response = await fetch(`http://localhost:5036/Course/${courseId}`);
                if (!response.ok) {
                    throw new Error('Network response was not ok ' + response.statusText);
                }

                const data = await response.json();
                return data
            } 
            catch (error) {
                console.error('There was a problem with the fetch operation:', error);
            }
        },

        async updateCourseInfo(courseId, request) {
            try {
                const response = await fetch(`http://localhost:5036/Course/main-info/${courseId}`, {
                    method: "PUT",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify(request),
                });
                if (!response.ok) {
                    throw new Error('Network response was not ok ' + response.statusText);
                }

                const result = await response.json();
                console.log("Курс успешно обновлен:", result);
            } 
            catch (error) {
                console.error('There was a problem with the fetch operation:', error);
            }
        },

        async showAllCourses() {
            const data = await this.getAllCourses();
            let inRow = 0;
            let cardsRow = [];
            
            data.forEach((course) => {
                if (inRow === 4) {
                    this.courseRows.push(cardsRow);
                    cardsRow = [];
                    inRow = 0;
                }
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

            const data = await this.getCourseInfo(courseId);

            this.form.courseId = courseId;
            this.form.image = data.photo;
            this.form.title = data.title;
            this.form.description = data.description;

            this.openModal();
        },

        async updateCourse() {

            const request = {
                photo: this.form.image,
                title: this.form.title,
                description: this.form.description
            }
            
            console.log(request);
            console.log(this.form.courseId);
            
            await this.updateCourseInfo(this.form.courseId, request);
            this.closeModal();

            await this.refreshCard();
        },

        async refreshCard() {
            const data = await this.getCourseInfo(this.form.courseId);
            
            const updatedCourse = {
                "id": this.form.courseId,
                "title": data.title,
                "image": data.photo,
                "description": data.description
            } 

            this.courseRows = this.courseRows.map(row =>
                row.map(course => 
                    (course.id === this.form.courseId ? { ...course, ...updatedCourse } : course))
             );
        },

        deleteCourse(event) {
            event.stopPropagation();
            console.log('delete');
        },

        createCourse() {

            this.form.courseId = null;
            this.form.image = null;
            this.form.title = null;
            this.form.description = null;

            this.openModal();
        },

        openModal() {
            this.isModalOpen = true;
        },

        closeModal() {
            this.isModalOpen = false;
        }


    },
};


</script>

<style scoped>

.container {
    margin-top: 10rem;
    width: 90%;
    justify-self: center;
}

.btns-container {
        margin-top: 15px;
        display: flex;
        gap: 35px;
    }
</style>