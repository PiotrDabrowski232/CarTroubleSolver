<template>
    <div class="accordion-container">
        <div class="accordion" id="accordionPanelsStayOpenExample" v-if="repairs">
            <div class="accordion-item" v-for="(repair, index) in repairs" :key="index">
                <h2 class="accordion-header">
                    <button class="accordion-button" type="button" data-bs-toggle="collapse"
                        :data-bs-target="'#collapse-' + index" aria-expanded="true"
                        :aria-controls="'collapse-' + index">
                        {{ formatDate(repair.date) }} - {{ getServiceName(repair.service) }}
                    </button>
                </h2>
                <div :id="'collapse-' + index" class="accordion-collapse collapse show">
                    <div class="accordion-body">
                        <div class="workshopInfo">
                            <p><strong>Workshop name:</strong> {{ repair.workshopName }}</p>
                            <p><strong>Workshop contact:</strong> {{ getPhoneNumber(repair.workshopNumber) }}</p>
                        </div>
                        <div class="ServiceinfoInfo">
                            <p><strong>Service:</strong> {{ getServiceName(repair.service) }}</p>
                            <p><strong>Service price:</strong> {{ repair.price }} PLN</p>
                            <p><strong>Working time:</strong> {{ repair.spentHours }} godz.</p>
                            <p><strong>Cost of parts:</strong></p>
                            <ul class="parts-list">
                                <li v-for="(item, idx) in repair.historyItems" :key="idx">
                                    {{ item.name }} - {{ item.price }} PLN x {{ item.quantity }}
                                </li>
                            </ul>
                        </div>
                        <div class="totals">
                            <p><strong>Total cost of the service:</strong> {{ totalServiceCost(repair) }} PLN</p>
                            <p><strong>Total cost of parts:</strong> {{ totalPartsCost(repair) }} PLN</p>
                            <p class="total-cost"><strong>Total cost of repair:</strong> {{ totalRepairCost(repair) }}
                                PLN</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { getAllRepairsHistory } from '@/services/StatusCommunication';

export default {
    name: 'RepairsHistory',
    props: {
        id: String
    },
    data() {
        return {
            repairs: []
        }
    },
    mounted() {
        this.getHistories()
    },
    methods: {
        async getHistories() {
            this.repairs = await getAllRepairsHistory(this.id);
        },
        getServiceName(service) {
            switch (service) {
                case "OilChange":
                    return "Oil Change";
                case "CarInspection":
                    return "Car Inspection";
                case "MechanicalService":
                    return "Mechanical Service";
                case "FilterReplacement":
                    return "Filter Replacement";
                case "FluidReplacement":
                    return "Fluid Replacement";
                default:
                    return "different service";
            }
        },
        formatDate(dateString) {
            const date = new Date(dateString);
            const day = date.getDate().toString().padStart(2, '0');
            const month = (date.getMonth() + 1).toString().padStart(2, '0');
            const year = date.getFullYear();
            return `${day}-${month}-${year}`;
        },
        totalServiceCost(repair) {
            return parseFloat((repair.price * repair.spentHours).toFixed(2));
        },
        totalPartsCost(repair) {
            return parseFloat(repair.historyItems.reduce((sum, item) => {
                return sum + (parseFloat(item.price || 0) * parseInt(item.quantity || 1));
            }, 0).toFixed(2));
        },
        totalRepairCost(repair) {
            return parseFloat((this.totalServiceCost(repair) + this.totalPartsCost(repair)).toFixed(2));
        },
        getPhoneNumber(phoneNumber) {
            let phoneStr = phoneNumber.toString();

            let formatted = phoneStr.replace(/(\d{3})(?=\d)/g, '$1 ');

            return `(+48) ${formatted}`;
        },
    }
}
</script>

<style scoped>
.accordion-container {
    max-width: 100%;
}

.workshopInfo {
    border-bottom: 0.0625rem solid #ddd;
}

.accordion-item {
    border: 0.0625rem solid #ddd;
    border-radius: 0.3125rem;
    margin-bottom: 0.625rem;
    overflow: hidden;
    box-shadow: 0 0.25rem 0.375rem rgba(0, 0, 0, 0.1);
}

.ServiceinfoInfo {
    margin-top: 0.625rem;
}

.accordion-button {
    font-size: 1.1rem;
    font-weight: bold;
    color: #333;
    background-color: #f8f9fa;
    padding: 0.625rem 1rem;
    border-bottom: 0.0625rem solid #ddd;
}

.accordion-button:hover {
    background-color: #e9ecef;
}

.accordion-body {
    padding: 1rem;
    background-color: #fff;
}

.parts-list {
    list-style-type: disc;
    padding-left: 1.25rem;
    margin-top: 0.3125rem;
    font-size: 1rem;
}

.parts-list li {
    margin: 0.1875rem 0;
    color: #555;
}

.totals {
    margin-top: 0.625rem;
    padding-top: 0.625rem;
    border-top: 0.0625rem solid #ddd;
}

.totals p {
    font-size: 1rem;
    margin: 0.375rem 0;
}

.total-cost {
    font-weight: bold;
    color: #007bff;
    font-size: 1.1rem;
}
</style>
