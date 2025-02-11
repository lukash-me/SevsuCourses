import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import './assets/styles/buttons.css';
import './assets/styles/forms.css';
import './assets/styles/info-modals.css';

const app = createApp(App);
app
.use(router)
.mount('#app');