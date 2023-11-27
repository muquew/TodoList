<template>
  <ul class="show_ul">
    <li v-for="(item, index) in Value" :key="index">
      <n-text 
        :delete="item.is_check" 
        :style="{
          color: font.fontColor,
          fontSize: font.fontSize
        }"
        v-if="isOrderList"
      >
        {{ index+1 }}.
      </n-text>
      <n-text 
        :delete="item.is_check" 
        :style="{
          color: font.fontColor,
          fontSize: font.fontSize
        }"
      >
        {{ item.content }}
      </n-text>
    </li>
  </ul>
</template>


<script>
import { NText} from 'naive-ui'
import { invoke } from "@tauri-apps/api/tauri"
import { listen } from '@tauri-apps/api/event'
import {setTimer} from '@/utils/timer'


export default {
    components: { NText},
    data() {
      return {
        Value: [],
        isOrderList: '',
        font: {
          fontColor: '',
          fontSize: '14px'
        }
      }
    },
    methods: {
      async getData() {
        this.Value = await invoke("get_user_items");
      }
    },
    async created() {
      this.getData()
      // let iniObject = await readIniFile();
      // this.isOrderList = (iniObject.settings.Orderedlist == 0 ? false : true)

      let Orderedlist = await invoke("read_from_config_user", {section: "show", key: "Orderedlist"});
      this.isOrderList = (Orderedlist == "0" ? false : true)
      let fontColor = await invoke("read_from_config_user", {section: "show", key: "fontColor"});
      this.font.fontColor = fontColor
      let fontSize = await invoke("read_from_config_user", {section: "show", key: "fontSize"});
      this.font.fontSize = fontSize+"px"


      await setTimer()


      await listen('change_orderList', async (event) => {     // Interface.vue
        this.isOrderList = (event.payload == 0 ? false : true)
      })
      await listen('change_fontColor', async (event) => {     // Interface.vue
        console.log(event.payload)
        this.font.fontColor = event.payload
      })
      await listen('change_fontSize', async (event) => {     // Interface.vue
        this.font.fontSize = event.payload+"px"
      })
      await listen('set_show_items',  async () => {          // Change.vue
        this.getData()
        await setTimer()
      })
    }
    

}
</script>

<style scoped>
.show_ul {
  list-style: none;
  padding: 0;
  margin: 0;
}
</style>