<template>
  <div style="padding: 10px;">
    <img src="/src-tauri/icons/32x32.png" />
    <text class="dio"> TodoList </text>
    <div>{{ label.Copyright }} © 2023 muquew.com</div>
    <div class="divider"></div>
    <div class="right">
      {{ label.Contact }}
      <a href="mailto:muquew@outlook.com">
        muquew@outlook.com
      </a>
    </div>
  </div>

</template>

<script>
export default {
  components: {},
  data() {
    return {
      label: {
        Copyright: '',
        Contact: ''
      },
    }
  },
  methods: {
    updateLabelLanuage() {
      this.label.Copyright = this.$store.getters.getLang.settings.About.Copyright
      this.label.Contact = this.$store.getters.getLang.settings.About.Contact
    },
  },
  async created() {
    this.updateLabelLanuage()
  }
}
</script>

<style scoped>
.divider {
  width: 100%;
  height: 1px;
  border-top: solid #3d3d3d 1px;
  margin: 5px 0;
}
.right {
  display: flex;
  justify-content: flex-end;
}
.dio {
  font-weight: bolder;
  font-size: 30px;
}
</style>