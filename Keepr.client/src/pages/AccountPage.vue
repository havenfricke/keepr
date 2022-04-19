<template>
  <div class="container-fluid bg-warning">
    <span class="row justify-content-center py-5 mx-3 mt-0 shadow bg-light">
      <img class="imgfix rounded img-fluid" :src="account.picture" alt="" />
      <div class="col-8 col-8-md col-12-sm">
        <div class="row justify-content-start text-truncate">
          <h1
            style="font-size: 22pt"
            class="col-12 tshadow p-2 text-start text-truncate"
          >
            {{ account.name }}
          </h1>
          <h5 class="col-12 tshadow text-start">Vaults: {{ vaultCount }}</h5>
          <h5 class="col-12 tshadow">Keeps: {{ userKeepCount }}</h5>
        </div>
      </div>
    </span>
    <div class="row mt-5">
      <h4
        class="
          col-lg-2 col-md-4
          tshadow
          col-sm-4
          p-1
          mx-3
          border-bottom border-dark
        "
      >
        My Vaults
        <b
          ><i
            style="font-size: 16pt"
            data-bs-target="#createVaultModal"
            data-bs-toggle="modal"
            title="Add a Vault"
            class="
              text-bottom
              px-1
              rounded
              mdi mdi-plus
              text-primary
              hoverable2
              tshadow
              fs-4
            "
          >
          </i
        ></b>
      </h4>
    </div>
    <div class="d-flex mx-5 justify-content-center">
      <div
        style="
          height: 80vh;
          width: 98vw;
          border: 0.7px solid gray;
          overflow: auto;
        "
        class="row bg-light rounded mt-3 shadow"
      >
        <div
          v-for="v in myvaults"
          :key="v.id"
          class="col-lg-3 col-md-3 col-sm-12 hoverable2 justify-content-center"
        >
          <VaultCard :vault="v" />
        </div>
      </div>
    </div>
    <div class="row mt-5">
      <h4
        class="
          col-lg-2 col-md-4 col-sm-4
          p-1
          mx-3
          mb-3
          border-bottom border-dark
          tshadow
          fs-4
        "
      >
        My Keeps
        <b
          ><i
            style="font-size: 16pt"
            data-bs-target="#createKeepModal"
            data-bs-toggle="modal"
            title="Add a Keep"
            class="
              text-bottom
              px-1
              rounded
              mdi mdi-plus
              text-primary
              hoverable2
              tshadow
            "
          >
          </i
        ></b>
      </h4>
    </div>
    <div class="masonry-columns mt-0 p-2">
      <div
        v-for="k in mykeeps"
        :key="k.id"
        class="col-12 rounded hoverable container"
      >
        <KeepCard2 :keep="k" />
      </div>
    </div>
    <Modal id="myAccModal">
      <template #body>
        <div class="container-fluid">
          <div class="row">
            <div class="col-6 d-flex justify-content-center">
              <img
                class="rounded shadow img-fluid object-fit"
                style="width: auto; height: 77vh"
                :src="activeKeep.img"
                alt=""
              />
            </div>
            <div class="col-6 border-start border-lg border-gray">
              <div class="row p-0 justify-content-center">
                <div class="col-3 text-center">
                  <i class="mdi text-center mdi-eye text-primary"
                    ><b class="text-center ms-1">{{ activeKeep.views }}</b></i
                  >
                </div>
                <div class="col-3 d-flex text-center align-items-start">
                  <span
                    class="
                      d-flex
                      container
                      rounded
                      justify-content-center
                      align-items-center
                    "
                  >
                    <img
                      class="rounded object-fit img-fluid"
                      style="height: 2vh; min-width: 2vh"
                      src="src\assets\img\keepr-logo.png"
                    /><b class="text-primary">
                      <em class="text-center ms-1">{{ activeKeep.kept }}</em>
                    </b>
                  </span>
                </div>
              </div>
              <div>
                <div class="row justify-content-center">
                  <h2 class="mt-4 fs-1 col-12 text-center">
                    {{ activeKeep.name }}
                  </h2>
                  <p class="col-12 px-5 fs-6">
                    {{ activeKeep.description }}
                  </p>
                  <div
                    class="border-bottom border-dark border-lg mt-5 col-6"
                  ></div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </template>
    </Modal>
    <Modal id="createVaultModal">
      <template #title>Create A Vault!</template>
      <template #body>
        <form @submit.prevent="createVault">
          <div class="row d-flex mx-2 align-items-center">
            <input
              required
              v-model="vaultData.name"
              class="col-12 rounded my-2 p-1"
              type="text"
              placeholder="Title..."
              minlength="1"
              maxlength="20"
            />
            <input
              required
              v-model="vaultData.img"
              class="col-12 rounded my-2 p-1"
              type="text"
              placeholder="Vault Image Link..."
            />
            <textarea
              required
              v-model="vaultData.description"
              class="col-12 rounded my-2 p-1"
              type="text"
              placeholder="Description..."
              cols="30"
              rows="6"
              minlength="5"
              maxlength="255"
            />

            <div class="row d-flex align-items-center justify-content-start">
              <label
                title="Set to private vault"
                class="switch col-4 mx-3 mt-2"
              >
                <input
                  title="Set to private vault"
                  v-model="vaultData.isPrivate"
                  type="checkbox"
                />
                <span class="slider round"></span>
              </label>

              <span class="col-8 fs-6 align-bottom text-stsrt"
                >Private Vault?</span
              >
            </div>
            <div class="row d-flex justify-content-end">
              <button
                data-bs-dismiss="modal"
                class="col-4 btn btn-success text-white mt-4"
              >
                Add
              </button>
            </div>
          </div>
        </form>
      </template>
    </Modal>
    <Modal id="createKeepModal">
      <template #title>Create A Keep!</template>
      <template #body>
        <form @submit.prevent="createKeep">
          <input
            required
            v-model="keepData.name"
            class="col-12 rounded my-2 p-1"
            type="text"
            placeholder="Title..."
            minlength="1"
            maxlength="20"
          />
          <input
            required
            v-model="keepData.img"
            class="col-12 rounded my-2 p-1"
            type="text"
            placeholder="Cover Image link..."
          />
          <textarea
            required
            v-model="keepData.description"
            class="col-12 rounded my-2 p-1"
            type="text"
            placeholder="Description..."
            cols="30"
            rows="6"
            minlength="5"
            maxlength="255"
          />
          <div class="row d-flex justify-content-end">
            <button
              data-bs-dismiss="modal"
              class="col-4 btn btn-success text-white mt-4 mx-3"
            >
              Add
            </button>
          </div>
        </form>
      </template>
    </Modal>
  </div>
</template>

<script>
import { computed, ref, watchEffect } from 'vue'
import { AppState } from '../AppState'
import { logger } from "../utils/Logger"
import { keepsService } from "../services/KeepsService"
import { vaultsService } from "../services/VaultsService"


export default {

  name: 'Account',
  setup() {
    const vaultData = ref({});
    const keepData = ref({});
    watchEffect(async () => {
      try {
        await keepsService.getUserKeeps()
      } catch (error) {
        logger.error(error)
      }
    })
    return {
      vaultData,
      keepData,

      async createVault() {
        try {
          await vaultsService.createVault(vaultData.value)
        } catch (error) {
          logger.error(error)
        }
      },
      async createKeep() {
        try {
          await keepsService.createKeep(keepData.value)
        } catch (error) {
          logger.error(error)
        }
      },
      account: computed(() => AppState.account),
      myvaults: computed(() => AppState.myvaults),
      vaultCount: computed(() => AppState.myvaults.length),
      mykeeps: computed(() => AppState.profilekeeps),
      userKeepCount: computed(() => AppState.profilekeeps.length),
      activeKeep: computed(() => AppState.activeKeep)
    }
  }
}
</script>

<style scoped>
.imgfix {
  width: 30vh;
  height: auto;
}
.hoverable:hover {
  transform: scale(1.03);
  filter: drop-shadow(0px 15px 10px rgba(0, 0, 0, 0.3));
  transition: 125ms ease-in-out;
  cursor: pointer;
}
.hoverable:active {
  transform: scale(0.98);
  transition: 50ms ease-in-out;
}
.hoverable2:hover {
  transform: scale(1.001);
  filter: drop-shadow(0px 15px 10px rgba(0, 0, 0, 0.3));
  transition: 125ms ease-in-out;
  cursor: pointer;
}
.hoverable2:active {
  transform: scale(0.98);
  transition: 50ms ease-in-out;
}
.masonry-columns {
  columns: 5 200px;
  column-gap: 0.1vw;
  img {
    display: inline-block;
    width: 99%;
  }
}
::-webkit-scrollbar {
  width: 20px;
  border-radius: 15px 50px 30px 5px;
}

::-webkit-scrollbar-track {
  background: #f1f1f100;
}

::-webkit-scrollbar-thumb {
  background: rgb(255, 119, 0);
}

::-webkit-scrollbar-thumb:hover {
  background: #555;
}
.switch {
  position: relative;
  display: inline-block;
  width: 48px;
  height: 22px;
}

.switch input {
  opacity: 0;
  width: 0;
  height: 0;
}

.slider {
  position: absolute;
  cursor: pointer;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: #ccc;
  -webkit-transition: 0.4s;
  transition: 0.4s;
}

.slider:before {
  position: absolute;
  content: "";
  height: 14px;
  width: 14px;
  left: 4px;
  bottom: 4px;
  background-color: white;
  -webkit-transition: 0.4s;
  transition: 0.4s;
}

input:checked + .slider {
  background-color: #ff7300;
}

input:focus + .slider {
  box-shadow: 0 0 1px #ff7300;
}

input:checked + .slider:before {
  -webkit-transform: translateX(26px);
  -ms-transform: translateX(26px);
  transform: translateX(26px);
}

/* Rounded sliders */
.slider.round {
  border-radius: 34px;
}

.slider.round:before {
  border-radius: 50%;
}
.tshadow {
  text-shadow: rgba(0, 0, 0, 0.204) 1px 2px 6px;
}
</style>
