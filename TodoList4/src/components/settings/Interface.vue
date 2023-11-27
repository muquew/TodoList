<template>
  <n-grid cols="1" y-gap="5">
    <n-grid-item>
      <n-checkbox      
        v-model:checked="formValue.orderList"
        @update:checked="orderListUpdateValue"
        size="medium"
        style="margin: 0 12px"
      >
        {{ label.orderList }}
      </n-checkbox>
    </n-grid-item>

    <n-grid-item>
      <n-grid cols="2" style="width: 180px">
        <n-grid-item style="width: 90px;">
          <span style="line-height: 34px; margin-left: 12px;">
            {{ label.fontColor }}
          </span>
        </n-grid-item>
        <n-grid-item style="width: 300px;">
          <n-color-picker
            v-model:value="formValue.fontColor"
            :on-complete="fontColorUpdateValue"
          />
        </n-grid-item>
      </n-grid>
    </n-grid-item>

    <n-grid-item>
      <n-grid cols="2" style="width: 180px">
        <n-grid-item style="width: 90px;">
          <span style="line-height: 34px; margin-left: 12px;">
            {{ label.fontSize }}
          </span>
        </n-grid-item>
        <n-grid-item style="width: 300px;" class="center">
          <n-slider 
            v-model:value="formValue.fontSize"
            :format-tooltip="formatTooltipFontSize"
            @update-value="fontSizeUpdateValue"
          />
        </n-grid-item>
      </n-grid>
    </n-grid-item>



  </n-grid>


  <!-- <pre>{{ JSON.stringify(formValue, null, 2) }} </pre> -->
</template>

<script>
import {NGrid, NGridItem, NCheckbox, NColorPicker, NSpace, NSlider} from 'naive-ui'
import { invoke } from "@tauri-apps/api/tauri";
import { emit } from '@tauri-apps/api/event'

export default {
  components: {NGrid, NGridItem, NCheckbox, NColorPicker, NSpace, NSlider},
  data() {
    return {
      formValue: {
        orderList: '',
        fontColor: '',
        fontSize: 0
      },
      label: {
        orderList: '',
        fontColor: '',
        fontSize: ''
      },
    }
  },
  methods: {
    updateLabelLanuage() {
      this.label.orderList = this.$store.getters.getLang.settings.Interface.orderList
      this.label.fontColor = this.$store.getters.getLang.settings.Interface.fontColor
      this.label.fontSize = this.$store.getters.getLang.settings.Interface.fontSize
    },
    async getFormValue() {
      // let iniObject = await readIniFile()
      // this.formValue.orderList = (iniObject.settings.Orderedlist==0 ? false : true)

      let Orderedlist = await invoke("read_from_config_user", {section: "show", key: "Orderedlist"});
      this.formValue.orderList = (Orderedlist== "0" ? false : true)
      let fontColor = await invoke("read_from_config_user", {section: "show", key: "fontColor"});
      this.formValue.fontColor = fontColor
      let fontSize = await invoke("read_from_config_user", {section: "show", key: "fontSize"});
      this.formValue.fontSize = parseInt(fontSize)
    },
    async orderListUpdateValue(value) {
      let inivalue = value ? "1" : "0"
      await invoke("change_config_user", {updates: [['show', 'Orderedlist', inivalue]]})
      emit('change_orderList', inivalue)    // Show.vue
    },
    async fontColorUpdateValue(value) {
      await invoke("change_config_user", {updates: [['show', 'fontColor', value]]})
      emit('change_fontColor', value)    // Show.vue
    },
    formatTooltipFontSize(value) {
      return `${value}px`
    },
    async fontSizeUpdateValue(value) {
      await invoke("change_config_user", {updates: [['show', 'fontSize', String(value)]]})
      emit('change_fontSize', value)    // Show.vue
    }
  },
  async created() {
    this.getFormValue()
    this.updateLabelLanuage()
  }
}
</script>

<style scoped>
.center {
  display: flex;
  justify-content: center;
  align-items: center;
}
</style>