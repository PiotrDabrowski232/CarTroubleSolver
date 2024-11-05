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
                    <td>{{item.name}}</td>
                    <td>
                        <p style="margin: 0px;" v-for="(subitem, index) in item.services"  :key="index">{{ subitem }}</p>    
                    </td>
                    <td v-if="item.rating !== 0">{{item.rating}}/5</td>
                    <td v-if="item.rating === 0">There is no ratings</td>
                    <td>{{item.city}}</td>
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
            console.log(this.Workshops)
        }
    }
}
</script>

<style>
.table tr td{
    margin: auto;
}
</style>