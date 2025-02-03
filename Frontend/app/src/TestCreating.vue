<template>
    

    <div class="testdiv">test</div>
    <form class="course-edit-form" @submit.prevent="handleSubmit" novalidate>
            
            <h1>Курс</h1>
            

            <div class="field">
                <span>Изображение</span>
                <input 
                id="image"
                
                type="text"
                class="box"
                placeholder="Укажите название изображения.."
                aria-errormessage="image-errors"
                title=""
                />
                <span class="field__errors" id="image-errors"></span>
            </div>

            <div class="field">
                <span>Название*</span>
                <input
                id="title"
                
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
                type="text" 
                class="box"
                placeholder="Краткое описание того, что ждет студента на курсе.."
                aria-errormessage="description-errors"
                title=""></textarea>
                <span class="field__errors" id="description-errors"></span>
            </div>

            <div class="btns-container">
                <button class="cancel-btn">Отменить</button>
                <button class="save-btn">Сохранить</button>
            </div>
    </form>





</template>

<script>


export default {
    name: "TestCreating",
    
    setup(){

        
    },

    methods() {
        const handleSubmit = () => {
            console.log("ok")
        }

        return {
            handleSubmit,
        };
    }
   
}
</script>

<style scoped>
    .testdiv{
        width: 600px;
        height: 600px;
        background-color: red;
    }

    .btns-container {
        margin-top: 15px;
        display: flex;
        gap: 35px;
    }
</style>