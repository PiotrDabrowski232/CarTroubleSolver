<template>
    <form>
        <select v-model="selectedBrand" class="form-select" aria-label="Default select example" @change="fetchModels">
            <option v-for="(brand, index) in brands" :key="index" :value="brand">
                {{ brand }}
            </option>
        </select>

        <select v-model="selectedModel" class="form-select" aria-label="Default select example">
            <option v-for="(model, index) in models" :key="index" :value="model">
                {{ model }}
            </option>
        </select>
    </form>
</template>

<script>
    import { fetchCarBrand, fetchCarModels } from "../../services/CarApiCommunication"

    export default {
        name: "CarForm",
        data() {
            return {
                brands: null,
                models: null,
                selectedBrand: null,
                selectedModel: null
            }
        },
        mounted() {
            this.fetchData()
        },
        methods: {
            async fetchData() {
                this.brands = await fetchCarBrand()
            },
            async fetchModels() {
                if (this.selectedBrand) {
                    this.models = await fetchCarModels(this.selectedBrand)
                }
            },
        },
    }
</script>

    
    
    
    
    
<style scoped>
.form-select{
    width: 10vw;
    margin-left: 10vw;
    margin-top: 10vh;

}
    
</style>
    