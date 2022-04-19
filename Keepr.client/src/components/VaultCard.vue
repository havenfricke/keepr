<template>
  <a @click="goTo('VaultPage')" class="row justify-content-center">
    <img
      class="img-fluid object-fit lockshadow"
      src="src\assets\img\1200px-Orange_lock.svg.png"
      alt=""
    />
    <h5
      style="transform: translateY(-12vh)"
      class="text-center col-7 text-clip text-white"
    >
      {{ vault.name }}
    </h5>
  </a>
</template>

<script>
import { useRouter } from "vue-router"
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
      goTo(page) {
        router.push({
          name: page,
          params: { id: props.vault.id }
        })
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
</style>