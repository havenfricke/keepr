<template>
  <a @click="goTo('VaultPage')">
    <img
      class="img-fluid object-fit"
      src="src\assets\img\1200px-Orange_lock.svg.png"
      alt=""
    />
    <h5
      style="transform: translateY(-7vh)"
      class="text-center text-truncate text-white"
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
</style>