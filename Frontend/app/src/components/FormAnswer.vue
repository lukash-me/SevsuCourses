<template>
    <div class="overlay">
              <form class="answer-send-form" @submit.prevent="handleSubmit" novalidate>

                <h1>Отправить ответ</h1>

                <div class="field">
                  <span>Введите текст ответа</span>
                  <textarea v-model="localForm.answerText"
                  class="box"
                  ></textarea>
                </div>
                
                <div class="btns-form-container">
                  <button class="cancel-btn" @click="$emit('close')">Вернуться</button>
                  <button class="save-btn" type="submit">Отправить</button>
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
                    answerText: localForm.answerText,
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

.btns-form-container {

  margin-top: 40px;
  margin-bottom: 40px;
  position: relative;
  align-self: center;
  display: flex;
  justify-content: center;
  gap: 30px;
}

h1 {
    font-size: 3rem;
}

h2 {
    font-size: 2rem;
}

</style>