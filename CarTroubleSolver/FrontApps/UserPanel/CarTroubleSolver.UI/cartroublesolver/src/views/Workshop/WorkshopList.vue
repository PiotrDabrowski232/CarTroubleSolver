<template>
    <div>
        <table class="table" v-if="Workshops.length !== 0">
            <thead>
                <tr>
                    <th scope="col">Workshop Name</th>
                    <th scope="col">Services</th>
                    <th scope="col">Rating</th>
                    <th scope="col">City</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(item, index) in Workshops"  :key="index">
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </tbody>
        </table>
    </div>
</template>


<script>
//import router from '@/router';
import { worskhops } from '@/services/UserApiCommunication';
export default {
    name: 'WorkshopList',
    data() {
        return {
            Workshops: []
        };
    },
    mounted(){
        this.GetWorkshopsData()
    },
    methods: {
        async GetWorkshopsData(){
            this.Workshops = await worskhops();
        }
    }
}
</script>