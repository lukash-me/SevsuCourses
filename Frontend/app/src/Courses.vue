<template>
    <div class="container">
        <div v-for="(cardsRow, index) in courseRows" :key="index" class="cards-row">
            <div
                v-for="(course) in cardsRow"
                :key="course.id"
                class="course-card"
                :data-id="course.id"
                @click="onCourseClick(course.id)"
                >
                <div class="image">
                    <img :src="`../images/${'abstract_02.jpg'}`" alt="Course Image" />
                </div>
                <div class="card-text">
                    <h1>{{ course.title }}</h1>
                    <span>{{ course.description }}</span>
                </div>
            </div>
            <div class="course-add-card">
                <div class="add-button" @click="onAddCourseClick">Добавить курс</div>
            </div>
        </div>
    </div>
</template>

<script>
import { useRouter } from 'vue-router';

export default {
    name: 'CoursesPage',

    setup() {
        const router = useRouter();
        return { router };
    },

    data() {
        return {
            courses: [],
            courseRows: [],
        };
    },

    mounted() {
        this.getAllCourses();
    },

    methods: {
        async getAllCourses() {
            try {
                const response = await fetch('http://localhost:5036/Course');
                if (!response.ok) {
                    throw new Error('Network response was not ok ' + response.statusText);
                }
                const data = await response.json();
                this.showAllCourses(data);
            } 
            catch (error) {
                console.error('There was a problem with the fetch operation:', error);
            }
        },
        showAllCourses(data) {
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
        onCourseClick(courseId) {
            console.log(this.router.getRoutes());
            this.router.push({ 
                name: 'themesPage',
                query: {id: courseId } 
            });
            
        },
        onAddCourseClick() {
        console.log("Добавить курс");
        },
    },
};


</script>

<style scoped>

.container {
    margin-top: 10rem;
    width: 90%;
    justify-self: center;
}
</style>