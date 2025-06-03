<template>
    <div class="overlay">
        <form class="reply-send-form" @submit.prevent="handleSubmit" novalidate>
            <h1>Оценивание ответа студента</h1>

            <div class="field">
                <span>Ответ студента</span>
                <textarea class="box" :value="answer.answerText" ></textarea>
            </div>

            <div class="field">
                <span>Эталонное решение</span>
                <textarea class="box" v-model="localForm.solutionText"></textarea>
            </div>

            <div class="field">
                <span>Ваш комментарий</span>
                <textarea class="box" v-model="localForm.replyText"></textarea>
            </div>

            <div class="field">
                <span>Оценить выполнение</span>
                <input class="markBox" v-model="localForm.mark" type="number" name="mark">
            </div>
            
            <div class="btns-form-container">
                <button class="cancel-btn" @click="$emit('close')"> Вернуться</button>
                <button class="save-btn" type="submit"> Отправить</button>
            </div>
        </form>
    </div>
</template>

<script>

    import { reactive } from 'vue';

    export default {

        props: {
            answer: Object,
            form: Object
        },

        setup(props, { emit }) {

            const localForm = reactive({ ...props.form });

            function handleSubmit() {

                const result = {
                    replyText: localForm.replyText,
                    mark: localForm.mark
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




<style>
    @import "../assets/styles/forms.css";
    @import "../assets/styles/buttons.css";
</style>