import { ref } from 'vue';
import { getTask } from '@/utils/requests/tasks'
import { isFailure } from '@/utils/shared/shared'

export class TasksController {

    tasks = ref();
    errors = ref();
    isLoading = ref();

    constructor() {
        this.tasks = [];
        this.errors = [];
        this.isLoading = false;
    }

    async loadTask(taskId) {
        this.reset();

        this.isLoading = true;

        const result = await getTask(taskId);

        if (isFailure(result)) {
            this.errors = result.errors;
            this.isLoading = false;
            return;
        }

        this.tasks = [result];
        this.isLoading = false;
        return;
    }

    reset() {
        this.tasks = [];
        this.errors = [];
    }
}