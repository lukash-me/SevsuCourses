import { ref } from 'vue';
import { getStudentMainInfo, getStudentsInfo } from '@/utils/requests/students'
import { isFailure } from '@/utils/shared/shared'

export class StudentsController {

    students = ref();
    errors = ref();
    isLoading = ref();

    constructor() {
        this.students = [];
        this.errors = [];
        this.isLoading = false;
    }

    async loadStudentMainInfo(studentId) {
        this.reset();

        this.isLoading = true;

        const result = await getStudentMainInfo(studentId);

        if (isFailure(result)) {
            this.errors = result.errors;
            this.isLoading = false;
            return;
        }

        this.students = [result];
        this.isLoading = false;
        return;
    }

    async loadStudentsInfo(request) {
        this.reset();

        this.isLoading = true;

        const result = await getStudentsInfo(request);

        if (isFailure(result)) {
            this.errors = result.errors;
            this.isLoading = false;
            return;
        }

        this.students = result;
        this.isLoading = false;
        return;
    }

    reset() {
        this.students = [];
        this.errors = [];
    }
}