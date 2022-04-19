<template>
  <div class="container-fluid">
    <div class="masonry-columns mt-0 p-2">
      <div
        v-for="k in keeps"
        :key="k.id"
        class="col-12 rounded hoverable container"
      >
        <KeepCard :keep="k" />
      </div>
    </div>
    <Modal id="myModal">
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
              <div class="row mt-5 pt-5 mx-2">
                <button
                  data-bs-toggle="modal"
                  data-bs-target="#addKeepModal"
                  class="col-6 text-white btn btn-primary shadow hoverable"
                >
                  Add {{ activeKeep.name }} to Vault
                </button>
              </div>
            </div>
          </div>
        </div>
      </template>
    </Modal>
    <Modal id="addKeepModal">
      <template #title>Select Vault</template>
      <template #body>
        <p>Please select the vault you would like add to.</p>
        <form @submit.prevent="createKV">
          <div class="row justify-content-center">
            <select v-model="kvBody" class="col-10" name="" id="">
              <option v-for="v in vaults" :key="v.id" :value="v.id">
                {{ v.name }}
              </option>
            </select>
            <button
              data-bs-toggle="modal"
              data-bs-target="#addKeepModal"
              class="col-7 mt-3 text-white btn btn-primary shadow hoverable"
            >
              Add {{ activeKeep.name }} to Vault
            </button>
          </div>
        </form>
      </template>
    </Modal>
  </div>
</template>

<script>
import { computed, ref } from "@vue/reactivity"
import { watchEffect } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import { keepsService } from "../services/KeepsService"
import { AppState } from "../AppState"
import { vaultsService } from "../services/VaultsService"
import { accountService } from "../services/AccountService"
export default {
  name: 'Home',
  setup() {
    const kvBody = ref('');
    watchEffect(async () => {
      try {
        await keepsService.getAllKeeps()
      } catch (error) {
        logger.error(error)
      }
    })
    return {
      kvBody,
      async createKV() {
        try {
          await vaultsService.createKV(kvBody.value)
        } catch (error) {
          logger.error(error)
        }
      },
      keeps: computed(() => AppState.keeps),
      activeKeep: computed(() => AppState.activeKeep),
      vaults: computed(() => AppState.vaults)
    }

  }
}
</script>

<style scoped lang="scss">
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
.masonry-columns {
  columns: 5 200px;
  column-gap: 0.1vw;
  img {
    display: inline-block;
    width: 99%;
  }
}
</style>
