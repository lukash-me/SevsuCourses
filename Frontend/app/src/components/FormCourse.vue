<template>

    <div class="overlay">
        <form class="course-edit-form" @submit.prevent="handleSubmit" novalidate>
            
            <h1>Курс</h1>
            

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
                <span>Описание</span>
                <textarea
                id="description"
                v-model="localForm.description"
                type="text" 
                class="box"
                placeholder="Краткое описание того, что ждет студента на курсе.."
                aria-errormessage="description-errors"
                title=""></textarea>
                <span class="field__errors" id="description-errors"></span>
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
                    description: localForm.description
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
    @import "../assets/styles/info-modals.css";
    @import "../assets/styles/forms.css";
    @import "../assets/styles/buttons.css";

    .container {
        display: flex;
        
        width: 90%;
        gap: 10px;
        flex-wrap: wrap;

        margin: 0 auto;
        margin-top: 10rem;
    }

    .btns-container {
        margin-top: 15px;
        display: flex;
        gap: 35px;
    }

</style>