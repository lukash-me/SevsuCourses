import { ref } from 'vue';
import { getSolution } from '@/utils/requests/solutions'
import { isFailure } from '@/utils/shared/shared'

export class SolutionsController {

    solutions = ref();
    errors = ref();
    isLoading = ref();

    constructor() {
        this.solutions = [];
        this.errors = [];
        this.isLoading = false;
    }

    async loadSolution(taskId) {
        this.reset();

        this.isLoading = true;

        const result = await getSolution(taskId);

        if (isFailure(result)) {
            this.errors = result.errors;
            this.isLoading = false;
            return;
        }

        this.solutions = [result];
        this.isLoading = false;
        return;
    }

    reset() {
        this.solutions = [];
        this.errors = [];
    }
}