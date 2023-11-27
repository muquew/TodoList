<template>
  <div class="show">
    <Show />
  </div>
  
</template>

<script>
import Show from '@/pages/Show.vue';
import {registerShortcut, unregisterShortcut} from "@/utils/shortcut"
export default {
  components:{ Show },
  async created() {
    await registerShortcut();
  },
  async beforeUnmount() {
    await unregisterShortcut();
  }
}
</script>

<style scoped>
.show {
  padding: 10px;
}
</style>