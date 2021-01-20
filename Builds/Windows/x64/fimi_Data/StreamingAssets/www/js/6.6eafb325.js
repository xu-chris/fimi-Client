(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([[6],{7383:function(t,e,a){"use strict";a.r(e);var s=function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("q-page",{staticClass:"q-pa-md"},[a("q-list",[a("q-item",[a("div",{staticClass:"text-h4"},[t._v("Training results for "+t._s(t.data.trainingName)+" "),a("q-chip",{attrs:{color:"grey-9","text-color":"white",label:t.getDurationInMinutesAsLabel(t.data.totalDurationInSeconds)}})],1)]),a("q-separator",{attrs:{spaced:""}}),a("q-item-label",{attrs:{header:""}},[t._v("Here's where you have improved")]),t._l(t.data.improvements,(function(e){return a("div",{key:"sm-"+e.name},[a("q-item",[a("q-item-section",{attrs:{avatar:""}},[a("q-icon",{attrs:{name:"arrow_upward",color:"green"}})],1),a("q-item-section",[a("q-item-label",[t._v(t._s(e.name))]),a("q-item-label",{attrs:{caption:"",lines:"2"}},[t._v(t._s(e.explanation))])],1)],1),e!=t.data.improvements.slice(-1)[0]?a("q-separator",{attrs:{spaced:"",inset:"item"}}):t._e()],1)})),a("q-separator",{attrs:{spaced:""}}),a("q-item-label",{attrs:{header:""}},[t._v("Things you should watch next time")]),t._l(t.data.ruleViolations,(function(e){return a("div",{key:"sm-"+e.name},[a("q-item",[a("q-item-section",{attrs:{avatar:""}},[a("q-icon",{attrs:{name:"error_outline",color:"yellow"}})],1),a("q-item-section",[a("q-item-label",[t._v(t._s(e.name))]),a("q-item-label",{attrs:{caption:"",lines:"2"}},[t._v(t._s(e.explanation))])],1)],1),e!=t.data.ruleViolations.slice(-1)[0]?a("q-separator",{attrs:{spaced:"",inset:"item"}}):t._e()],1)}))],2),a("q-footer",{staticClass:"bg-grey-10 q-pa-sm text-center",attrs:{bordered:""}},[a("div",{staticClass:"row q-gutter-sm"},[a("div",{staticClass:"col"},[a("q-btn",{staticClass:"full-width",attrs:{color:"primary",outline:"",label:"Schedule next",disabled:""}})],1),a("div",{staticClass:"col"},[a("q-btn",{staticClass:"full-width",attrs:{color:"primary",label:"Back to trainings"},on:{click:function(e){return t.returnToStart()}}})],1)])]),a("q-inner-loading",{attrs:{showing:t.loading}},[a("q-spinner-ios",{attrs:{size:"30px",color:"white"}})],1)],1)},i=[],r={name:"PostTraining",computed:{loading:{get(){return this.$store.state.data.loading}},data:{get(){return this.$store.state.data.trainingResults}},userId:{get(){return this.$store.state.data.userId}},userData:{get(){return localStorage.userData}}},watch:{userId(t){this.getTrainingResults()}},beforeMount(){this.getUserData(),this.getTrainingResults(),this.$store.commit("data/setLoading",!0)},methods:{returnToStart:function(){this.$store.dispatch("data/backButtonClicked")},getTrainingResults:function(){null!=this.userId&&this.$store.dispatch("data/getTrainingResults",this.userId)},getUserData:function(){null!=this.userId&&this.$store.dispatch("data/getUserData",this.userId)},getDurationInMinutesAsLabel:function(t){var e=Math.floor(t/60),a=t%60;return(0!==e?e+" min":"")+(0!==a?" "+a+" sec":"")}}},n=r,o=a("2877"),l=a("9989"),c=a("1c1c"),u=a("66e5"),d=a("b047"),m=a("eb85"),g=a("0170"),h=a("4074"),p=a("0016"),q=a("7ff0"),b=a("9c40"),v=a("74f7"),f=a("d9b2"),_=a("eebe"),w=a.n(_),I=Object(o["a"])(n,s,i,!1,null,null,null);e["default"]=I.exports;w()(I,"components",{QPage:l["a"],QList:c["a"],QItem:u["a"],QChip:d["a"],QSeparator:m["a"],QItemLabel:g["a"],QItemSection:h["a"],QIcon:p["a"],QFooter:q["a"],QBtn:b["a"],QInnerLoading:v["a"],QSpinnerIos:f["a"]})}}]);