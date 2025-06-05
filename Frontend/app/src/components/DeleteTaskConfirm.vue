<template>

    <div class="overlay">
        <div class="delete-confirm">
            <div class="text-modal">
                <span>Вы <b>уверены</b>, что хотите</span>
                <span><span class="red-font">удалить</span> выбранную задачу?</span>
            </div>
            <div class="btns-container">
                <button class="cancel-btn" @click="$emit('close')">Вернуться</button>
                <button class="delete-btn-big" @click="$emit('delete')">Удалить</button>
            </div>
        </div>
    </div>

</template>

<style scoped>
    @import "../assets/styles/info-modals.css";
    @import "../assets/styles/forms.css";
    @import "../assets/styles/buttons.css";
</style>