<template>
  <div class="row">
    <div class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1"
      aria-labelledby="staticBackdropLabel" aria-hidden="true">
      <Toast v-if="showToast" :msg="toastMsg" :color="toastColor" />
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h1 class="modal-title fs-5" id="staticBackdropLabel">Change your password</h1>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <CustomInput type="password" label="Current Password" v-model="ResetPassword.CurrentPassword" :width='15' />
            <CustomInput type="password" label="New Password" :error="Errors.ResetPassword.NewPassword"
              v-model="ResetPassword.NewPassword" :width='15' />
            <CustomInput type="password" label="New Confirmed Password"
              :error="Errors.ResetPassword.NewConfirmedPassword" v-model="ResetPassword.NewConfirmedPassword"
              :width='15' />
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            <button type="button" class="btn btn-primary" @click="Password()">Change</button>
          </div>
        </div>
      </div>
    </div>

    <div class="modal fade" id="staticServices" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1"
      aria-labelledby="staticBackdropLabel" aria-hidden="true">
      <Toast v-if="showToast" :msg="toastMsg" :color="toastColor" />
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h1 class="modal-title fs-5" id="staticBackdropLabel">Update Services</h1>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <table class="table" v-if="ProvidedServices.length > 0">
              <thead>
                <tr>
                  <th scope="col">Service Type</th>
                  <th scope="col">Price per hour</th>
                  <th scope="col">Remove Service</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(service, index) in AddServicesBase" :key="index">
                  <td style="text-align: center;">{{ service.serviceType }}</td>
                  <td style="text-align: center;">{{ service.price }}</td>
                  <td style="text-align: center;"><button type="button" class="btn btn-outline-primary"
                      @click="RemoveFromList(AddService)"><i class="bi bi-patch-minus"></i></button></td>
                </tr>
              </tbody>
            </table>
            <SelectInput v-model="AddService.serviceType" label="Province" type="text" :dataArray="ServiceTypes"
              :width='15' />
            <CustomInput type="number" label="Service Price" v-model="AddService.price" :width='15' />
            <button type="button" class="btn btn-primary" @click="AddToList(AddService)">Add To List</button>
          </div>

          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            <button type="button" class="btn btn-primary" @click="SaveServices()">Save</button>
          </div>
        </div>
      </div>
    </div>


    <div class="col-md-8">
      <div class="account-details-container">
        <div class="info-wrapper">
          <div class="account-info">
            <h2>User Account</h2>
            <p><strong>Name:</strong> {{ Workshop.name }}</p>
            <p><strong>Email:</strong> {{ Workshop.email }}</p>
            <p><strong>Phone Number:</strong> {{ Workshop.phoneNumber }}</p>
            <p><strong>NIP:</strong> {{ Workshop.nip }}</p>
          </div>
          <div class="services-info" v-if="ProvidedServices.length > 0">
            <h2>Provided Services</h2>
            <ul>
              <li v-for="(service, index) in ProvidedServices" :key="index">
                {{ service.serviceType }}: {{ service.price }} zł/h
              </li>
            </ul>
          </div>
        </div>
        <div class="address-info">
          <h2>Address</h2>
          <p><strong>Street:</strong> {{ Workshop.adress.streetName }} {{ Workshop.adress.streetNumber }}</p>
          <p><strong>Postal Code:</strong> {{ Workshop.adress.postalCode }}</p>
          <p><strong>City:</strong> {{ Workshop.adress.city }}</p>
          <p><strong>Province:</strong> {{ Workshop.adress.province }}</p>
          <p><strong>Country:</strong> {{ Workshop.adress.country }}</p>
        </div>

        <div class="button-group mt-2">
          <button class="btn btn-primary me-2" data-bs-toggle="modal" data-bs-target="#staticBackdrop">Change
            Password</button>
          <button class="btn btn-warning me-2" data-bs-toggle="modal" data-bs-target="#staticServices"
            @click.prevent="TakeServices()">Update Services</button>
          <button class="btn btn-secondary">Some Actions</button>
        </div>
      </div>
    </div>

    <div class="col-md-4 d-flex justify-content-center align-items-center">
      <div class="opening-hours">
        <button class="btn btn-warning btn-block mb-3 w-100" v-on:click="changeSite()">Change Opening Hours</button>
        <ul class="list-group">
          <li class="list-group-item">Monday <p>{{this.DisplayHours("Monday")}}</p>
          </li>
          <li class="list-group-item">Tuesday <p>{{this.DisplayHours("Tuesday")}}</p>
          </li>
          <li class="list-group-item">Wednesday <p>{{this.DisplayHours("Wednesday")}}</p>
          </li>
          <li class="list-group-item">Thursday <p>{{this.DisplayHours("Thursday")}}</p>
          </li>
          <li class="list-group-item">Friday <p>{{this.DisplayHours("Friday")}}</p>
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>


<script>
  import {
    getFromLocalStorage
  } from '@/Services/LocalStorageService';
  import CustomInput from '@/components/Form/CustomInput.vue';
  import router from '@/router';
  import {
    ResetPassword
  } from '@/ApiCommunication/workshop';
  import Toast from '@/components/Toast.vue';
  import SelectInput from '@/components/Form/SelectInput.vue';
  import {
    GetServiceTypes,
    AddServices,
    GetService
  } from '@/ApiCommunication/Service';

  export default {
    name: 'AccountDetails',
    components: {
      CustomInput,
      Toast,
      SelectInput
    },
    data() {
      return {
        Workshop: getFromLocalStorage("Workshop") || {},
        ResetPassword: {
          CurrentPassword: "",
          NewPassword: "",
          NewConfirmedPassword: ""
        },
        Errors: {
          ResetPassword: {}
        },
        showToast: false,
        toastMsg: '',
        toastColor: '',
        ServiceTypes: {
          serviceType: "",
          price: 0,
        },
        AddService: {
          serviceType: "",
          price: 0,
        },
        ProvidedServices: [],
        AddServicesBase:[]
      };
    },
    async mounted() {
      try {
        this.ProvidedServices = await GetService(this.Workshop.id);
      } catch (error) {
        console.error('Error fetching services:', error);
      }
    },
    methods: {
      changeSite() {
        router.push("/Hours")
      },
      showToastMessage(message, color) {
        this.toastMsg = message;
        this.showToast = true;
        this.toastColor = color;
        setTimeout(() => {
          this.showToast = false;
        }, 3000);
      },
      DisplayHours(dayName) {
        const hoursForDay = this.Workshop.hours.find(x => x.dayOfWeek == dayName)
        if (hoursForDay) {
          return `${hoursForDay.from} - ${hoursForDay.to}`;
        } else {
          return "--";
        }
      },
      async Password() {
        try {
          await ResetPassword(this.ResetPassword);
          this.showToastMessage('Password changed', 'success');
          this.ResetPassword.CurrentPassword = ""
          this.ResetPassword.NewPassword = ""
          this.ResetPassword.NewConfirmedPassword = ""

        } catch (ex) {
          console.log(ex);
          this.Errors.ResetPassword = {};
          if (ex.response && ex.response.data && ex.response.data.errors) {
            this.Errors.ResetPassword = ex.response.data.errors;
          }
          this.showToastMessage('Invalid Password', 'danger');
        }
      },
      AddToList(service) {
        const newService = {
          serviceType: service.serviceType,
          price: service.price,
        };

        this.AddServicesBase.push(newService);

        this.AddService.serviceType = '';
        this.AddService.price = 0;
      },
      RemoveFromList(index) {
        this.AddServicesBase.splice(index, 1);
      },
      async TakeServices() {
        this.ServiceTypes = await GetServiceTypes()
        this.AddServicesBase = []
        this.ProvidedServices.forEach(x => {
          this.AddServicesBase.push(x)
        })
      },
      async SaveServices() {
        try {
          await AddServices(this.Workshop.id, this.AddServicesBase);

          this.ProvidedServices = this.AddServicesBase;

          this.ProvidedServices = []
          this.AddServicesBase.forEach(x => {
          this.ProvidedServices.push(x)
        })

          this.showToastMessage('Services Added', 'success');

        } catch (error) {
          console.error(error);
          this.showToastMessage('Failed to add services', 'danger');
        }
      }
    },
  }
</script>

<style scoped>
  @import './AccountDetails.css';
</style>