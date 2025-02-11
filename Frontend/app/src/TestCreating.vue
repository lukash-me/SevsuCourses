<template>
    
    <div class="testdiv">test</div>

    
    





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
        
        margin-top: 30px;
        display: flex;
        gap: 35px;
    }

    
</style>