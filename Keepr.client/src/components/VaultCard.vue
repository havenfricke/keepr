<template>
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
</template>

<script>
export default {
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup() {
    return {}
  }
}
</script>

<style scoped>
img {
  overflow-x: hidden;
}
</style>