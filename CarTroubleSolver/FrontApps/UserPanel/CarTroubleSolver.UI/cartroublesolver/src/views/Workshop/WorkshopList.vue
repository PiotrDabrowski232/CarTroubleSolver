<template>
    <div>
        <div class="filters">
            <div class="left-controls">
                <label for="cityFilter">Filter by City:</label>
                <select id="cityFilter" v-model="selectedCity" @change="filterByCity">
                    <option value="">All Cities</option>
                    <option v-for="city in uniqueCities" :key="city" :value="city">{{ city }}</option>
                </select>

                <label for="sortBy">Sort By:</label>
                <select id="sortBy" v-model="sortOption" @change="sortWorkshops">
                    <option value="">None</option>
                    <option value="city">City</option>
                    <option value="rating">Rating</option>
                </select>
            </div>

            <div class="right-controls">
                <button @click="calculateDistance">Calculate Distance</button>
            </div>
        </div>

        <table class="table" v-if="filteredWorkshops.length !== 0">
            <thead>
                <tr>
                    <th scope="col">Workshop Name</th>
                    <th scope="col">Services</th>
                    <th scope="col">Rating</th>
                    <th scope="col">City</th>
                    <th scope="col">Distance</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(item, index) in filteredWorkshops" :key="index">
                    <td><a class="workshop-link" v-on:click="WorkshopDetails(item.id)">{{ item.name }}</a></td>
                    <td>
                        <p style="margin: 0px;" v-for="(subitem, index) in item.services" :key="index">{{ subitem }}</p>
                    </td>
                    <td v-if="item.rating !== 0"><a class="workshop-link" v-on:click="WorkshopDetails(item.id)">{{
                            item.rating }}/5</a></td>
                    <td v-if="item.rating === 0">There is no ratings</td>
                    <td>{{ item.city }}</td>
                    <td v-if="item.distance"> {{ (item.distance).toFixed(2) }} km </td>
                    <td v-else> ? km</td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
import { worskhops } from '@/services/UserApiCommunication';
import { getLocation } from '@/services/Geolocalization';
import {saveToLocalStorage} from '@/LocalStorage/useLocalStorage';

export default {
    name: 'WorkshopList',
    data() {
        return {
            Workshops: [],
            selectedCity: "",
            sortOption: "",
            filteredWorkshops: []
        };
    },
    computed: {
        uniqueCities() {
            return [...new Set(this.Workshops.map(workshop => workshop.city))];
        }
    },
    mounted() {
        this.GetWorkshopsData();
    },
    methods: {
        async GetWorkshopsData() {
            this.Workshops = await worskhops();
            this.filteredWorkshops = this.Workshops;
        },
        filterByCity() {
            if (this.selectedCity) {
                this.filteredWorkshops = this.Workshops.filter(workshop => workshop.city === this.selectedCity);
            } else {
                this.filteredWorkshops = this.Workshops;
            }
        },
        sortWorkshops() {
            if (this.sortOption === "city") {
                this.filteredWorkshops.sort((a, b) => a.city.localeCompare(b.city));
            } else if (this.sortOption === "rating") {
                this.filteredWorkshops.sort((a, b) => b.rating - a.rating);
            }
        },
       async calculateDistance() {
                const position = await getLocation();
                const geo = {
                    Latitude: position.coords.latitude,
                    Longitude: position.coords.longitude,
                }
                saveToLocalStorage(`geoLocation`, geo);
                this.GetWorkshopsData();
        },
        WorkshopDetails(id) {
            console.log(id)
            this.$router.push({ name: "WorkshopDetails", params: { id: id } });
        }
    }
}
</script>

<style>
.filters {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 10px 0;
    margin-bottom: 15px;
    border-bottom: 2px solid #ddd;
}

.left-controls {
    display: flex;
    align-items: center;
    gap: 10px;
}

.right-controls button {
    padding: 8px 15px;
    background-color: #4CAF50;
    color: white;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    font-weight: bold;
    transition: background-color 0.3s ease;
    margin-right: 2vw;
}

.right-controls button:hover {
    background-color: #45a049;
}

.filters label {
    font-weight: bold;
    margin-right: 5px;
    color: #333;
}

.filters select {
    padding: 6px;
    border: 1px solid #ccc;
    border-radius: 5px;
    transition: border-color 0.3s ease;
}

.filters select:focus {
    outline: none;
    border-color: #4CAF50;
}

.workshop-link {
    color: #2a6f97;
    text-decoration: none;
    cursor: pointer;
    font-weight: bold;
    transition: color 0.3s ease, text-decoration 0.3s ease;
}

.workshop-link:hover {
    color: #1f4d64;
    text-decoration: underline;
}

.table tr td {
    margin: auto;
}
</style>
