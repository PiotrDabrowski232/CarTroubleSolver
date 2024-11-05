<template>
    <div class="message-details">
        <Toast position="top-center"/>
        <header>
            <h2>📩 Message from the workshop</h2>
            <p><strong>Workshop:</strong> {{ MessageDetails.workshopName }}</p>
            <p><strong>Date of dispatch:</strong> {{ formatDate(MessageDetails.sentAt) }}</p>
        </header>

        <section class="message-content">
            <p v-if="MessageDetails.message"><strong>Wiadomość:</strong> {{ MessageDetails.message }}</p>
            <p><strong>Date of visit:</strong> {{ formatDate(MessageDetails.dateOfVisit) }}</p>
        </section>

        <section v-if="MessageDetails.car" class="car-info">
            <h3>🚗 Information about the car</h3>
            <ul>
                <li><strong>Brand:</strong> {{ MessageDetails.car.brand }}</li>
                <li><strong>Model:</strong> {{ MessageDetails.car.model }}</li>
                <li><strong>VIN:</strong> {{ MessageDetails.car.vin }}</li>
                <li><strong>Milleage:</strong> {{ MessageDetails.car.mileage }} km</li>
                <li><strong>Engine:</strong> {{ MessageDetails.car.engine }}</li>
            </ul>
        </section>

        <section class="service-info">
            <p><strong>Service:</strong> {{ MessageDetails.service }}</p>
        </section>

        <div class="button-group">
            <button @click="acceptMessage" class="accept-button">✔ Accept</button>
            <button @click="rejectMessage" class="reject-button">✘ Reject</button>
        </div>
    </div>
</template>

<script>
import { setMessageRead, GetMessageFullInfo } from '@/services/UserApiCommunication';
import { AddAccident } from '@/services/AccidentCommunication';

export default {
    name: 'MessageDetails',
    props: {
        id: String,
    },
    data() {
        return {
            MessageDetails: {}
        };
    },
    mounted() {
        this.getFullMessage();
        this.setUserMessageRead();
    },
    methods: {
        async setUserMessageRead() {
            await setMessageRead(this.id);
        },
        async getFullMessage() {
            this.MessageDetails = await GetMessageFullInfo(this.id);
            console.log(this.MessageDetails);
        },
        formatDate(date) {
            const options = { year: 'numeric', month: '2-digit', day: '2-digit' };
            return new Date(date).toLocaleDateString(undefined, options);
        },
        async acceptMessage() {
            var result = await AddAccident(this.id, true);
            if(result){
                this.Toast("Accident Added. Check more info in car details");
                setTimeout(() => {
                    this.$router.push("/")
                }, 3000);
            }
        },
        async rejectMessage() {
            var result = await AddAccident(this.id, false);
            if(result){
                this.Toast("Response send successfuly");
                setTimeout(() => {
                    this.$router.push("/")
                }, 3000);
            }
        },
        Toast(message){
        return this.$toast.add({severity: 'success' , summary: message , life: 3000 });
    }

    }
};
</script>

<style>
.message-details {
    padding: 20px;
    max-width: 600px;
    margin: 20px auto;
    border-radius: 10px;
    box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);
    background-color: #ffffff;
    font-family: Arial, sans-serif;
}

header h2 {
    font-size: 1.5rem;
    color: #333;
    border-bottom: 2px solid #ddd;
    padding-bottom: 8px;
    margin-bottom: 10px;
}

.message-content,
.car-info,
.service-info {
    margin: 15px 0;
    padding: 15px;
    border-radius: 8px;
}

.message-content {
    background-color: #f8f8f8;
}

.car-info {
    background-color: #f1f9ff;
    border: 1px solid #d0e7ff;
}

.service-info {
    font-weight: bold;
    color: #5a5a5a;
    text-align: center;
}

.car-info h3 {
    margin-top: 0;
    color: #3178c6;
    font-size: 1.2rem;
}

.car-info ul {
    padding: 0;
    list-style: none;
}

.car-info li {
    padding: 4px 0;
    font-size: 0.95rem;
}

.button-group {
    display: flex;
    justify-content: space-around;
    margin-top: 20px;
}

.accept-button,
.reject-button {
    padding: 10px 25px;
    font-size: 1rem;
    font-weight: bold;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    transition: all 0.3s;
}

.accept-button {
    background-color: #4CAF50;
    color: white;
}

.reject-button {
    background-color: #f44336;
    color: white;
}

.accept-button:hover {
    background-color: #45a049;
    transform: scale(1.05);
}

.reject-button:hover {
    background-color: #d73a2c;
    transform: scale(1.05);
}
</style>