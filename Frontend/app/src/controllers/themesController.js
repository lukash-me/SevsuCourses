import { ref } from 'vue';
import { getTheme } from '@/utils/requests/themes'
import { isFailure } from '@/utils/shared/shared'

export class ThemesController {

    themes = ref();
    errors = ref();
    isLoading = ref();

    constructor() {
        this.themes = [];
        this.errors = [];
        this.isLoading = false;
    }

    async loadTheme(themeId) {
        this.reset();

        this.isLoading = true;

        const result = await getTheme(themeId);

        if (isFailure(result)) {
            this.errors = result.errors;
            this.isLoading = false;
            return;
        }

        this.themes = [result];
        this.isLoading = false;
        return;
    }

    reset() {
        this.themes = [];
        this.errors = [];
    }
}