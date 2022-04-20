<template>
  <div class="container-fluid bg-warning">
    <span class="row justify-content-center py-5 mx-3 mt-0 shadow bg-light">
      <img
        class="imgfix rounded img-fluid"
        :src="activeProfile.picture"
        alt=""
      />
      <div class="col-8 col-8-md col-12-sm">
        <div class="row justify-content-start text-truncate">
          <h1
            style="font-size: 22pt"
            class="col-12 tshadow p-2 text-start text-truncate"
          >
            {{ activeProfile.name }}
          </h1>
          <h5 class="col-12 tshadow text-start">Vaults: {{ vaultcount }}</h5>
          <h5 class="col-12 tshadow">Keeps: {{ keepcount }}</h5>
        </div>
      </div>
    </span>
    <div class="row mt-5">
      <h4
        class="
          col-lg-3 col-md-6
          tshadow
          col-sm-4
          p-1
          mx-3
          border-bottom border-dark
        "
      >
        {{ activeProfile.name }}'s Vaults
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
          v-for="v in profilevaults"
          :key="v.id"
          class="col-lg-2 col-md-3 col-sm-12 hoverable2 justify-content-center"
        >
          <VaultCard2 :vault="v" />
        </div>
      </div>
    </div>
    <div class="row mt-5">
      <h4
        class="
          col-lg-3 col-md-4 col-sm-6
          p-1
          mx-3
          mb-3
          border-bottom border-dark
          tshadow
          fs-4
        "
      >
        {{ activeProfile.name }}'s Keeps
      </h4>
    </div>
    <div class="masonry-columns mt-0 p-2">
      <div
        v-for="k in profilekeeps"
        :key="k.id"
        class="col-12 rounded hoverable container"
      >
        <KeepCard2 :keep="k" />
      </div>
    </div>
    <Modal id="profileModal">
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
                  <h2 class="mt-4 text-break fs-1 col-12 text-center">
                    {{ activeKeep.name }}
                  </h2>
                  <p class="col-12 text-break px-3 fs-6">
                    {{ activeKeep.description }}
                  </p>
                  <div
                    class="border-bottom border-dark border-lg mt-5 col-6"
                  ></div>
                </div>
              </div>
              <div class="row mt-3 pt-5 mx-2">
                <button
                  data-bs-toggle="modal"
                  data-bs-target="#addKeepModal"
                  class="
                    col-6
                    text-white text-break
                    btn btn-primary
                    shadow
                    hoverable
                  "
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
        <p class="text-center">
          Please select the vault you would like add to.
        </p>
        <form @submit.prevent="createKV">
          <div class="row justify-content-center">
            <select required v-model="kvBody" class="col-10" name="" id="">
              <option v-for="v in myvaults" :key="v.id" :value="v.id">
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
import { computed, ref, watchEffect } from "@vue/runtime-core"
import { useRoute } from "vue-router"
import { logger } from "../utils/Logger"
import { profilesService } from "../services/ProfilesService"
import { AppState } from "../AppState"
import Pop from "../utils/Pop"
import { vaultsService } from "../services/VaultsService"
export default {
  name: 'ProfilePage',
  setup() {
    const kvBody = ref('');
    const route = useRoute()
    if (route.params.id) {
      watchEffect(async () => {
        try {
          await profilesService.getProfileById(route.params.id)
          await profilesService.getVaultsByProfileId(route.params.id)
          await profilesService.getKeepsByProfileId(route.params.id)
        } catch (error) {
          logger.error(error)
        }
      });
    }
    return {
      kvBody,
      async createKV() {
        try {
          if (Pop.toast('Keep added to vault!', 'success', '1A2B3C')) {
            await vaultsService.createKV(kvBody.value)
          }
        } catch (error) {
          logger.error(error)
        }
      },
      activeProfile: computed(() => AppState.activeProfile),
      profilevaults: computed(() => AppState.profilevaults),
      profilekeeps: computed(() => AppState.profilekeeps),
      vaultcount: computed(() => AppState.profilevaults.length),
      keepcount: computed(() => AppState.profilekeeps.length),
      activeKeep: computed(() => AppState.activeKeep),
      myvaults: computed(() => AppState.myvaults)
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