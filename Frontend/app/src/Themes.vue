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

</template>

<script>
    import { ref, onMounted } from 'vue';
    import { useRoute, useRouter } from 'vue-router';

    export default {
        name: "ThemesPage",
    setup() {
        const courseTitle = ref('');
        const themes = ref([]);
        const route = useRoute();
        const router = useRouter();
        const themeImage = '../images/themes_01.jpg';

        const getCourseTitle = async (courseId) => {
            try {
                const response = await fetch(`http://localhost:5036/Course/title-teacher/${courseId}`);
                if (!response.ok) {
                    throw new Error('Failed to fetch course');
                }
                const data = await response.json();
                courseTitle.value = data.title;
            } catch (error) {
                console.error('Error fetching course:', error);
            }
        };

        const getThemes = async (courseId) => {
            try {
                const response = await fetch(`http://localhost:5036/Theme/all/${courseId}`);
                if (!response.ok) {
                    throw new Error('Failed to fetch themes');
                }
                const data = await response.json();

                const enrichedThemes = await Promise.all(
                    data.map(async (theme) => {
                        const tasks = await fetchTasks(theme.id);
                        return { ...theme, tasks };
                    })
                );
                themes.value = enrichedThemes.sort((a, b) => a.number - b.number);
            } 
            catch (error) {
                console.error('Error fetching themes:', error);
            }
        };

        const fetchTasks = async (themeId) => {
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
        };

        const goToTask = (taskId) => {
        router.push({ name: 'taskPage', query: { id: taskId } });
        };

        onMounted(async () => {
            const courseId = route.query.id;
            if (courseId) {
                await getCourseTitle(courseId);
                await getThemes(courseId);
            } 
            else {
                console.error('No course ID provided');
            }
        });

        return {
        courseTitle,
        themes,
        themeImage,
        goToTask,
        };
    },
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
</style>