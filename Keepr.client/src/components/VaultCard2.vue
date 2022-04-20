<template>
  <a @click="goTo('VaultPage', 'Home')" class="row justify-content-center">
    <img
      class="img-fluid object-fit lockshadow"
      src="src\assets\img\1200px-Orange_lock.svg.png"
      alt=""
    />
    <h5
      style="transform: translateY(-10vh)"
      class="text-center tshadow col-8 text-clip text-white"
    >
      {{ vault.name }}
    </h5>
  </a>
</template>

<script>
import { useRouter } from "vue-router"
import { AppState } from "../AppState";
export default {
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const router = useRouter();
    return {
      goTo(page, page2) {
        if (props.vault.isPrivate == true) {
          router.push({
            name: page2
          })
        } else {
          AppState.activeVault = {}
          router.push({
            name: page,
            params: { id: props.vault.id }
          });
        }
      }
    }
  }
}
</script>

<style scoped>
img {
  overflow-x: hidden;
}
.lockshadow {
  filter: drop-shadow(5px 10px 4px #00000032);
}
.tshadow {
  text-shadow: rgba(0, 0, 0, 0.204) 1px 2px 6px;
}
</style>