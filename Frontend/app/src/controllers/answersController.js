import { ref } from 'vue';
import { getLastAnswer, createAnswer, createReply } from '@/utils/requests/answers'
import { isFailure } from '@/utils/shared/shared'
import { Answer } from '@/utils/models/answer'

export class AnswersController {

    answers = ref();
    errors = ref();
    isLoading = ref();

    constructor() {
        this.answers = [];
        this.errors = [];
        this.isLoading = false;
    }

    async loadAnswer(taskId, studentId) {
        this.reset();

        this.isLoading = true;

        const result = await getLastAnswer(taskId, studentId);

        if (isFailure(result)) {
            this.errors = result.errors;
            this.isLoading = false;
            return;
        }

        const answer = new Answer(result.id, null, null, result.mark, result.replyText, result.answerText, result.dateSent, result.dateReplied);

        this.answers = [answer];
        this.isLoading = false;
        return;
    }

    async createAnswer(request) {
        this.reset();

        this.isLoading = true;

        const result = await createAnswer(request);

        if (isFailure(result)) {
            this.errors = result.errors;
            this.isLoading = false;
            return;
        }

        this.answers = [result];
        this.isLoading = false;
        return;
    }

    async createReply(answerId, request) {
        this.reset();

        this.isLoading = true;

        const result = await createReply(answerId, request);

        if (isFailure(result)) {
            this.errors = result.errors;
            this.isLoading = false;
            return;
        }

        this.answers = [result];
        this.isLoading = false;
        return;
    }

    reset() {
        this.errors = [];
        this.answers = [];
    }
}