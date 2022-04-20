<template>
  <div class="container-fluid">
    <div class="row mx-2 mt-4 d-flex justify-content-around">
      <h2 class="col-8 border-bottom border-dark">{{ activeVault.name }}</h2>
      <i
        v-if="account.id == activeVault.creatorId"
        data-bs-toggle="modal"
        data-bs-target="#editVaultModalv"
        title="Edit vault"
        class="mdi col-2 fs-3 hoverable mdi-cog"
      ></i>
      <button
        @click="removeVault"
        v-if="account.id == activeVault.creatorId"
        class="col-2 btn btn-danger"
      >
        Delete Vault
      </button>
    </div>
    <div class="masonry-columns mt-0 p-2">
      <div
        v-for="k in vaultkeeps"
        :key="k.id"
        class="col-12 rounded hoverable container"
      >
        <KeepCard3 :keep="k" />
      </div>
    </div>
    <Modal id="KVModal">
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
                <button
                  v-if="account.id == activeVault.creatorId"
                  @click="removeKV"
                  data-bs-dismiss="modal"
                  class="
                    col-6
                    text-white
                    btn btn-danger
                    shadow
                    hoverable
                    text-break
                  "
                >
                  Remove Keep From Vault
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
    <Modal id="editVaultModalv">
      <template #title>Edit Vault</template>
      <template #body>
        <form @submit.prevent="editVault">
          <div class="row d-flex mx-2 align-items-center">
            <input
              v-model="vaultData.name"
              class="col-12 rounded my-2 p-1"
              type="text"
              placeholder="Title..."
              minlength="1"
              maxlength="20"
            />
            <input
              v-model="vaultData.img"
              class="col-12 rounded my-2 p-1"
              type="text"
              placeholder="Vault Image Link..."
            />
            <textarea
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
                Submit Changes
              </button>
            </div>
          </div>
        </form>
      </template>
    </Modal>
  </div>
</template>

<script>
import { computed, ref, watchEffect } from "@vue/runtime-core"
import { useRoute } from "vue-router";
import { keepsService } from "../services/KeepsService";
import { logger } from "../utils/Logger";
import { AppState } from "../AppState";
import { vaultsService } from "../services/VaultsService";
import Pop from "../utils/Pop";
export default {
  name: 'VaultPage',
  setup() {
    const route = useRoute();
    const vaultData = ref({});
    if (route.params.id) {
      watchEffect(async () => {
        try {
          await keepsService.getVaultKeeps(route.params.id)
          await vaultsService.getVaultById(route.params.id)
        } catch (error) {
          logger.log(error)
        }
      });
    }
    return {
      vaultData,
      async removeVault() {
        if (await Pop.confirm(`Are you sure You want to Delete ${this.activeVault.name}?`, 'You might regret it', 'warning')) {
          try {
            vaultsService.removeVault(route.params.id)
          } catch (error) {
            logger.error(error)
          }
        }
      },
      async removeKV() {
        if (await Pop.confirm(`Are you sure You want to remove ${this.activeKeep.name} from ${this.activeVault.name}?`, 'You might regret it', 'warning')) {
          try {
            await keepsService.removeKV(this.activeKeep.id, this.vkId.vaultKeepId)
          } catch (error) {
            logger.error(error)
          }
        }
      },
      async editVault() {
        try {
          await vaultsService.editVault(vaultData.value, route.params.id)
        } catch (error) {
          logger.error(error)
        }
      },
      vaultkeeps: computed(() => AppState.vaultkeeps),
      activeVault: computed(() => AppState.activeVault),
      account: computed(() => AppState.account),
      activeKeep: computed(() => AppState.activeKeep),
      myvaults: computed(() => AppState.myvaults),
      vkId: computed(() => AppState.vaultkeeps.find(vk => AppState.activeKeep.id == vk.id))
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
</style>
