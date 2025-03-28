import { createRouter, createWebHistory } from 'vue-router';
import loginPage from './Login.vue';
import coursesPage from './Courses.vue';
import themesPage from './Themes.vue';
import taskPage from './Task.vue';
import groupsPage from './Groups.vue';
import testCreating from './TestCreating.vue';
import adminPage from './Admin.vue';

const routes = [
    {
        path: '/login-page',
        name: 'loginPage',
        component: loginPage,
    },
    {
        path: '/courses-page',
        name: 'coursesPage',
        component: coursesPage,
    },
    {
        path: '/themes-page',
        name: 'themesPage',
        component: themesPage,
    },
    {
        path: '/task-page',
        name: 'taskPage',
        component: taskPage,
    },
    {
        path: '/test-creating',
        name: 'testCreating',
        component: testCreating,
    },
    {
        path: '/groups-page',
        name: 'groupsPage',
        component: groupsPage,
    },
    {
        path: '/admin-page',
        name: 'adminPage',
        component: adminPage,
    },
];

const router = createRouter({ 
    history: createWebHistory(process.env.BASE_URL), 
    routes  
});

export default router;