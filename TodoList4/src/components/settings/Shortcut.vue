<template>
  <div>{{ label.one }}</div>
  <div>{{ label.two }}</div>
  <div>{{ label.three }}</div>
</template>

<script>
export default {
  data() {
    return {
      label: ""
    }
  },
  created() {
    this.label = this.$store.getters.getLang.Todo.shortcut
  }
}
</script>

<!-- <template>
    <n-grid cols="1" y-gap="5">

      <n-grid-item>
        <n-grid cols="2" style="width: 400px">
          <n-grid-item style="width: 200px;">
            <span style="line-height: 34px; margin-left: 12px;">
              {{ label.change }}
            </span>
          </n-grid-item>
          <n-grid-item style="width: 200px;">
            <n-input
              clearable
              passively-activated
              readonly
              placeholder=""
              :value="shortcut.change.join('+')"
              :on-clear="clearPressKeys"
              :on-focus="startListen"
              :on-blur="handleBlur"
            >
            </n-input>
          </n-grid-item>
        </n-grid>
      </n-grid-item>

  </n-grid>
</template>

<script>
import {NGrid, NGridItem, NInput} from 'naive-ui'
export default {
  components: { NGrid, NGridItem, NInput },
  data() {
    return {
      isInFocus: true,
      shortcut: {
        change: []
      },
      label: {
        change: '',
      },
    };
  },
  created() {
      this.updateLabelLanuage()
  },
  methods: {
    updateLabelLanuage() {
      this.label.change = this.$store.getters.getLang.settings.Shortcut.changeWindow
    },
    clearPressKeys() {
      this.shortcut.change = []
    },
    startListen() {
      window.addEventListener('keydown', this.handleKeyDown);
      window.addEventListener('focus', this.handleFocus);
      window.addEventListener('blur', this.handleBlur);
    },
    stopListen() {
      window.removeEventListener('keydown', this.handleKeyDown);
      window.removeEventListener('focus', this.handleFocus);
      window.removeEventListener('blur', this.handleBlur);
    },
    handleKeyDown(event) {
      // if(event.key == "F5") event.preventDefault();
      const key = event.key;
      if (!this.shortcut.change.includes(key)) {
        this.shortcut.change.push(key);
      }
    },
    handleFocus() {
      this.isInFocus = true;
    },
    handleBlur() {
      this.isInFocus = false;
      this.startListen()
      this.shortcut.change = []
    },
  },
};
</script> -->