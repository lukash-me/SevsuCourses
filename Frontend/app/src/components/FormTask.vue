<template>

    <div class="overlay">
        <form class="task-edit-form" @submit.prevent="handleSubmit" novalidate>
            
            <h1>Задача</h1>
            
            <div class="field">
                <span>Изображение</span>
                <input 
                id="photo"
                v-model="localForm.photo"
                type="text"
                class="box"
                placeholder="Укажите название изображения.."
                aria-errormessage="photo-errors"
                title=""
                />
                <span class="field__errors" id="photo-errors"></span>
            </div>

            <div class="field">
                <span>Название*</span>
                <input
                id="title"
                v-model="localForm.title"
                type="text" 
                class="box"
                placeholder="Введите название.."
                aria-errormessage="title-errors"
                title=""
                required
                />
                <span class="field__errors" id="title-errors"></span>
            </div>

            <div class="field">
                <span>Условие задачи</span>
                <textarea
                id="description"
                v-model="localForm.condition"
                type="text" 
                class="box"
                placeholder="Текст задачи.."
                aria-errormessage="description-errors"
                title=""></textarea>
                <span class="field__errors" id="description-errors"></span>
            </div>

            <!-- <div class="field">
                <span>Количество попыток</span>
                <input
                id="attemps"
                v-model="formTask.attemps"
                type="text" 
                class="box"
                placeholder="Введите число.."
                aria-errormessage="attemps-errors"
                title="">
                <span class="field__errors" id="attemps-errors"></span>
            </div> -->

            <div class="field">
                <span>Минимальная оценка для выполнения</span>
                <input
                id="minMark"
                v-model="localForm.minMark"
                type="text" 
                class="box"
                placeholder="Введите число.."
                aria-errormessage="minMark-errors"
                title="">
                <span class="field__errors" id="minMark-errors"></span>
            </div>

            <div class="field">
                <span>Максимальная оценка</span>
                <input
                id="maxMark"
                v-model="localForm.maxMark"
                type="text" 
                class="box"
                placeholder="Введите число.."
                aria-errormessage="maxMark-errors"
                title="">
                <span class="field__errors" id="maxMark-errors"></span>
            </div>

            <div class="btns-container">
                <button class="cancel-btn" @click="$emit('close')">Отменить</button>
                <button class="save-btn" type="submit">Сохранить</button>
            </div>
        </form>
    </div>

</template>

<script>

    import { reactive } from 'vue';

    export default {

        props: {
            form: Object,
        },

        setup(props, { emit } ) {

            const localForm = reactive({ ...props.form });

            function handleSubmit() {

                const result = {
                    photo: localForm.photo,
                    title: localForm.title,
                    condition: localForm.condition,
                    minMark: localForm.minMark,
                    maxMark: localForm.maxMark
                }

                emit("submit", result);
            }

            return {
                localForm,
                handleSubmit
            };
        }
    }

</script>

<style scoped>
    @import "../assets/styles/info-modals.css";
    @import "../assets/styles/forms.css";
    @import "../assets/styles/buttons.css";
</style>