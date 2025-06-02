import { ref } from 'vue';
import { getAllGroupsOnCourse, getMentorGroupsOnCourse } from '@/utils/requests/groups'
import { isFailure } from '@/utils/shared/shared'

export class GroupsController {

    groups = ref();
    errors = ref();
    isLoading = ref();

    constructor() {
        this.groups = [];
        this.errors = [];
        this.isLoading = false;
    }

    async loadAllGroupsOnCourse(courseId) {
        this.reset();

        this.isLoading = true;

        const result = await getAllGroupsOnCourse(courseId);

        if (isFailure(result)) {
            this.errors = result.errors;
            this.isLoading = false;
            return;
        }

        this.groups = result;

        

        this.isLoading = false;
        return;
    }

    async loadMentorGroupsOnCourse(mentorId, courseId) {
        this.reset();

        this.isLoading = true;

        const result = await getMentorGroupsOnCourse(mentorId, courseId);

        if (isFailure(result)) {
            this.errors = result.errors;
            this.isLoading = false;
            return;
        }

        this.groups = result;
        this.isLoading = false;
        return;
    }

    reset() {
        this.groups = [];
        this.errors = [];
    }
}