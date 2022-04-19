<template>
  <div class="container-fluid">
    <span class="row justify-content-center py-5 mx-3 mt-2 shadow bg-light">
      <img class="imgfix rounded img-fluid" :src="account.picture" alt="" />
      <div class="col-8 col-8-md col-12-sm">
        <div class="row justify-content-start text-truncate">
          <h1
            style="font-size: 26pt"
            class="col-12 p-2 text-start text-truncate"
          >
            {{ account.name }}
          </h1>
          <h4 class="col-12 text-start">Vaults: {{ vaultCount }}</h4>
          <h4 class="col-12">Keeps: {{ userKeepCount }}</h4>
        </div>
      </div>
    </span>
    <div class="row mt-5">
      <h4 class="col-4 p-1 mx-3 border-bottom border-dark">
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
            "
          >
          </i
        ></b>
      </h4>
    </div>
    <div class="d-flex justify-content-center">
      <div
        style="
          height: 55vh;
          width: 98vw;
          border: 1px solid #ccc;
          overflow: auto;
        "
        class="row mt-3"
      >
        <div
          v-for="v in myvaults"
          :key="v.id"
          class="col-lg-3 col-md-6 col-sm-12 hoverable2 justify-content-center"
        >
          <VaultCard :vault="v" />
        </div>
      </div>
    </div>
    <div class="row mt-5">
      <h4 class="col-4 p-1 mx-3 mb-3 border-bottom border-dark">
        My Keeps
        <b
          ><i
            style="font-size: 16pt"
            data-bs-target="#createKeepModal"
            data-bs-toggle="modal"
            title="Add a Vault"
            class="
              text-bottom
              px-1
              rounded
              mdi mdi-plus
              text-primary
              hoverable2
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
      <template #title></template>
      <template #body></template>
    </Modal>
    <Modal id="createKeepModal">
      <template #title></template>
      <template #body></template>
    </Modal>
  </div>
</template>

<script>
import { computed, watchEffect } from 'vue'
import { AppState } from '../AppState'
import { logger } from "../utils/Logger"
import { keepsService } from "../services/KeepsService"
import { useRouter } from "vue-router"


export default {

  name: 'Account',
  setup() {

    watchEffect(async () => {
      try {
        await keepsService.getUserKeeps()
      } catch (error) {
        logger.error(error)
      }
    })
    return {
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
</style>
