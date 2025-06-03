<template>
    <div class="overlay">
        <div class="delete-confirm" id="rights">
            <div class="text-modal">
                <span>У вас<span class="red-font"> нет прав</span> для</span>
                <span> <b>выполнения</b> данного действия</span>
            </div>
            <div class="btns-container">
                <button class="cancel-btn" @click="$emit('close');">Увы</button>
            </div>
        </div>
    </div>
</template>

<style scoped>
@import "../assets/styles/info-modals.css";
@import "../assets/styles/forms.css";
</style>