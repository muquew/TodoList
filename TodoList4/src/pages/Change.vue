<template>
  <n-scrollbar style="max-height: 100vh">



    <n-space justify="space-around" size="large" class="buttons">
        <n-button-group class="buttonGroup">
            <n-button ghost @click="empty_items">
                {{ buttonlabel.empty }}
            </n-button>
            <n-button ghost @click="delete_blank_item">
                {{ buttonlabel.deleteBlank }}
            </n-button>
        </n-button-group>

        <n-button-group class="buttonGroup">
            <n-button @click="sort_items">
                {{ buttonlabel.sort }}
                <n-grid cols="1" style="padding-left: 4px;">
                    <n-grid-item>
                        <svg t="1700816632981" class="icon sort_up" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="9407">
                            <path d="M541.866667 285.866667l345.6 345.6c17.066667 17.066667 17.066667 42.666667 0 59.733333-8.533333 8.533333-19.2 12.8-29.866667 12.8H168.533333c-23.466667 0-42.666667-19.2-42.666666-42.666667 0-10.666667 4.266667-21.333333 12.8-29.866666l343.466666-345.6c17.066667-17.066667 42.666667-17.066667 59.733334 0z" 
                                :fill="fillColor1" p-id="9408">
                            </path>
                        </svg>
                    </n-grid-item>
                    <n-grid-item>
                        <svg t="1700816484973" class="icon sort_down" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="9083">
                            <path d="M482.133333 738.133333L136.533333 392.533333c-17.066667-17.066667-17.066667-42.666667 0-59.733333 8.533333-8.533333 19.2-12.8 29.866667-12.8h689.066667c23.466667 0 42.666667 19.2 42.666666 42.666667 0 10.666667-4.266667 21.333333-12.8 29.866666L541.866667 738.133333c-17.066667 17.066667-42.666667 17.066667-59.733334 0z" 
                                :fill="fillColor2" p-id="9084">
                            </path>
                        </svg>
                    </n-grid-item>
                </n-grid>
            </n-button>
        </n-button-group>

        <n-button-group class="buttonGroup">
            <n-button ghost @click="set_items">
                {{ buttonlabel.confirm }}
            </n-button>
            <n-button ghost @click="get_items">
                {{ buttonlabel.cancel }}
            </n-button>
        </n-button-group>
    </n-space>






    <n-dynamic-input
        show-sort-button
        v-model:value="Value" 
        :on-create="onCreate"
        class="nInput"
    >
        <template #default="{ value }">
            <div style="display: flex; align-items: center;">
                <n-checkbox
                    v-model:checked="value.is_check"
                    size="medium"
                    style="margin: 0 12px"
                />
                <n-input
                    v-model:value="value.content" 
                    type="text"
                    style="width: 400px;"
                    :placeholder="placeholders.input"
                />
                <n-date-picker
                    v-model:value="value.time"
                    :format="formatTime"
                    type="datetime" 
                    clearable
                    style="width: 300px;"
                    :placeholder="placeholders.datetime"
                    update-value-on-close
                    :is-date-disabled="disableDateTimeRange"
                    :is-time-disabled="disableDateTimeRange"
                    :time-picker-props="time_picker_props"
                />
            </div>
        </template>
    </n-dynamic-input>
    <!-- <pre>{{ JSON.stringify(Value, null, 2) }}</pre> -->


    <n-back-top bottom="20" right="20" style="background-color: #13c767;">
      <svg width="24" height="24" viewBox="0 0 48 48" fill="none">
        <path d="M43 7H5M24 20v23M17.547 21L24 13.96 30.453 21H17.547z" stroke="#eee" stroke-width="3"/>
        <path d="M24 14l-6 7h12l-6-7z" fill="#eee"/>
      </svg>
    </n-back-top>
  </n-scrollbar>


</template>

<script>
import {NScrollbar, NBackTop, NDynamicInput, NCheckbox, NInput, NDatePicker, NButtonGroup, NButton, NGrid, NGridItem, NSpace} from 'naive-ui'
import { listen, emit } from '@tauri-apps/api/event'
import { invoke } from "@tauri-apps/api/tauri"
export default {
  components: {NScrollbar, NBackTop, NDynamicInput, NCheckbox, NInput, NDatePicker, NButtonGroup, NButton, NGrid, NGridItem, NSpace},
  data() {
      return {
          Value: [],
          formatTime: "yyyy-MM-dd - HH:mm",
          time_picker_props: {
            seconds: [0]
          },
          typeOptions: "",
          placeholders: {},
          buttonlabel: {},
          blankItem: {
              is_check: false,
              content: '',
              time: null
          },
          onCreate() {
              return {
                  is_check: false,
                  content: '',
                  time: null
              };
          },
          fillColor1: "#666666",
          fillColor2: "#666666",
          fillColorDeactive: "#666666",
          fillColorActive: "#36AD9A",
          issort: 0
      }
  },
  methods: {
      getData() {
          this.getData_Lang()
          this.get_items()
      },
      getData_Lang() {
          this.getPlaceholder()
          this.getButtonLable()
      },
      getPlaceholder() {
          this.placeholders = this.$store.getters.getLang.dynamicInput.Placeholder
      },
      getButtonLable() {
          this.buttonlabel = this.$store.getters.getLang.dynamicInput.buttons
      },
      disableDateTimeRange(ts) {
        return ts <= Date.now();
      },
      async set_items() {
          await invoke("set_user_items", {data: this.Value});
          emit('set_show_items')
      },
      async get_items() {
          let items = await invoke("get_user_items");
          this.Value = items
      },
      empty_items() {
          this.Value = []
      },
      delete_blank_item() {
          this.Value = this.Value.filter(obj => {
              return obj.is_check !== this.blankItem.is_check ||
                  obj.content !== this.blankItem.content ||
                  obj.time !== this.blankItem.time;
          });
      },
      sort_items() {
          if(this.issort == 0) {
              this.sort_by_time_ascending()
          } else if(this.issort == 1) {
              this.sort_by_time_deascending()
          }
          
      },
      sort_by_time_ascending() {
          this.Value.sort((a, b) => a.time - b.time)
          this.fillColor1 = this.fillColorActive
          this.fillColor2 = this.fillColorDeactive
          this.issort = 1
      },
      sort_by_time_deascending() {
          this.Value.sort((a, b) => b.time - a.time)
          this.fillColor2 = this.fillColorActive
          this.fillColor1 = this.fillColorDeactive
          this.issort = 0
      }
  },
  async created() {
        // await this.$store.dispatch('initLang');
        // console.log(this.$Langs)
        // console.log(this.$store.getters.getLang)
        // this.$store.commit('setLang', 'en')
        // console.log(this.$store.getters.getLang)
        
        this.getData()
        await listen('change_language_frontend', (event) => {
            this.$store.commit('setLang', event.payload)
            this.getData_Lang()
        })
        // this.set_items();
        // this.getDafaultValue()
        // this.set_items()
  },
  // async beforeUpdate() {
  //     await invoke("set_user_items", {data: this.defaultValue});
  // }    
}
</script>

<style scoped>

.buttons {
    margin: 20px 0;
    background-color: #F0F0F0;
}
.buttonGroup {
    background-color: #FFF;
    border: 3px;
}
.sort_up {
  margin-bottom: -10px;
}
.sort_up, .sort_down {
  width: 20px;
  height: 20px;
}
.nInput {
  background-color: #F0F0F0;
  width: 90%;
  margin: 5px 5%;
}
</style>