
EXTRA_DIST = Makefile

# Warning: This is an automatically generated file, do not edit!

srcdir=.
top_srcdir=../

include $(top_srcdir)/Makefile.include
include $(top_srcdir)/config.make

#include $(srcdir)/custom-hooks.make

ifeq ($(CONFIG),DEBUG)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -debug -d:DEBUG
ASSEMBLY = ./bin/Debug/FacturaNet.FnGtk.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = exe
PROJECT_REFERENCES =  \
	../FacturaNet.FnNegocio/bin/Debug/FacturaNet.FnNegocio.dll \
	../AmUtil/bin/Debug/AmUtil.dll
BUILD_DIR = ./bin/Debug

FIREBIRDSQL_DATA_FIREBIRDCLIENT_DLL_SOURCE=../lib/FirebirdSql.Data.FirebirdClient.dll
FACTURANETGTK_DESKTOP_SOURCE=./app.desktop
FACTURANET_FNNEGOCIO_DLL_SOURCE=../FacturaNet.FnNegocio/bin/Debug/FacturaNet.FnNegocio.dll
FACTURANET_FNNEGOCIO_DLL_MDB_SOURCE=../FacturaNet.FnNegocio/bin/Debug/FacturaNet.FnNegocio.dll.mdb
FACTURANET_FNNEGOCIO_DLL_MDB=$(BUILD_DIR)/FacturaNet.FnNegocio.dll.mdb
AMUTIL_DLL_SOURCE=../AmUtil/bin/Debug/AmUtil.dll
AMUTIL_DLL_MDB_SOURCE=../AmUtil/bin/Debug/AmUtil.dll.mdb
AMUTIL_DLL_MDB=$(BUILD_DIR)/AmUtil.dll.mdb

endif

ifeq ($(CONFIG),RELEASE)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4
ASSEMBLY = ./bin/Release/FacturaNet.FnGtk.exe
ASSEMBLY_MDB = 
COMPILE_TARGET = exe
PROJECT_REFERENCES =  \
	../FacturaNet.FnNegocio/bin/Release/FacturaNet.FnNegocio.dll \
	../AmUtil/bin/Release/AmUtil.dll
BUILD_DIR = ./bin/Release

FIREBIRDSQL_DATA_FIREBIRDCLIENT_DLL_SOURCE=../lib/FirebirdSql.Data.FirebirdClient.dll
FACTURANETGTK_DESKTOP_SOURCE=./app.desktop
FACTURANET_FNNEGOCIO_DLL_SOURCE=../FacturaNet.FnNegocio/bin/Release/FacturaNet.FnNegocio.dll
FACTURANET_FNNEGOCIO_DLL_MDB=
AMUTIL_DLL_SOURCE=../AmUtil/bin/Release/AmUtil.dll
AMUTIL_DLL_MDB=

endif


PROGRAMFILES = \
	$(FIREBIRDSQL_DATA_FIREBIRDCLIENT_DLL) \
	$(FACTURANET_FNNEGOCIO_DLL) \
	$(FACTURANET_FNNEGOCIO_DLL_MDB) \
	$(AMUTIL_DLL) \
	$(AMUTIL_DLL_MDB)  

LINUX_DESKTOPAPPLICATIONS = \
	$(FACTURANETGTK_DESKTOP)  

BINARIES = \
	$(FACTURANETGTK)  



FIREBIRDSQL_DATA_FIREBIRDCLIENT_DLL = $(BUILD_DIR)/FirebirdSql.Data.FirebirdClient.dll
FACTURANETGTK = $(BUILD_DIR)/facturanetgtk
FACTURANETGTK_DESKTOP = $(BUILD_DIR)/facturanetgtk.desktop
FACTURANET_FNNEGOCIO_DLL = $(BUILD_DIR)/FacturaNet.FnNegocio.dll
AMUTIL_DLL = $(BUILD_DIR)/AmUtil.dll


FILES = \
	gtk-gui/generated.cs \
	Main.cs \
	AssemblyInfo.cs \
	FrmLogin.cs \
	FrmPrincipal.cs \
	gtk-gui/FacturaNet.FnGtk.FrmPrincipal.cs 

DATA_FILES = \
	app.desktop 

RESOURCES = \
	gtk-gui/gui.stetic 

EXTRAS = \
	facturanetgtk.in 

REFERENCES =  \
	-pkg:gtk-sharp-2.0 \
	-pkg:glib-sharp-2.0 \
	-pkg:glade-sharp-2.0 \
	System \
	Mono.Posix

DLL_REFERENCES =  \
	../lib/FirebirdSql.Data.FirebirdClient.dll

CLEANFILES += $(PROGRAMFILES) $(LINUX_DESKTOPAPPLICATIONS) $(BINARIES) 

#Targets
all: $(ASSEMBLY) $(PROGRAMFILES) $(LINUX_DESKTOPAPPLICATIONS) $(BINARIES)  $(top_srcdir)/config.make

$(FIREBIRDSQL_DATA_FIREBIRDCLIENT_DLL): $(FIREBIRDSQL_DATA_FIREBIRDCLIENT_DLL_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(FACTURANETGTK): facturanetgtk
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'
	chmod u+x '$@'

$(FACTURANETGTK_DESKTOP): $(FACTURANETGTK_DESKTOP_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(FACTURANET_FNNEGOCIO_DLL): $(FACTURANET_FNNEGOCIO_DLL_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(FACTURANET_FNNEGOCIO_DLL_MDB): $(FACTURANET_FNNEGOCIO_DLL_MDB_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(AMUTIL_DLL): $(AMUTIL_DLL_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(AMUTIL_DLL_MDB): $(AMUTIL_DLL_MDB_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'



facturanetgtk: facturanetgtk.in $(top_srcdir)/config.make
	sed -e "s,@prefix@,$(prefix)," -e "s,@PACKAGE@,$(PACKAGE)," < facturanetgtk.in > facturanetgtk


$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(build_resx_resources) : %.resources: %.resx
	resgen2 '$<' '$@'

$(ASSEMBLY) $(ASSEMBLY_MDB): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list)
	make pre-all-local-hook prefix=$(prefix)
	mkdir -p $(dir $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
	make post-all-local-hook prefix=$(prefix)


install-local: $(ASSEMBLY) $(ASSEMBLY_MDB) $(FIREBIRDSQL_DATA_FIREBIRDCLIENT_DLL) $(FACTURANETGTK) $(FACTURANETGTK_DESKTOP) $(FACTURANET_FNNEGOCIO_DLL) $(FACTURANET_FNNEGOCIO_DLL_MDB) $(AMUTIL_DLL) $(AMUTIL_DLL_MDB)
	make pre-install-local-hook prefix=$(prefix)
	mkdir -p $(prefix)/lib/$(PACKAGE)
	cp $(ASSEMBLY) $(ASSEMBLY_MDB) $(prefix)/lib/$(PACKAGE)
	test -z '$(FIREBIRDSQL_DATA_FIREBIRDCLIENT_DLL)' || cp $(FIREBIRDSQL_DATA_FIREBIRDCLIENT_DLL) $(prefix)/lib/$(PACKAGE)
	mkdir -p $(prefix)/bin
	test -z '$(FACTURANETGTK)' || cp $(FACTURANETGTK) $(prefix)/bin
	mkdir -p $(prefix)/share/applications
	test -z '$(FACTURANETGTK_DESKTOP)' || cp $(FACTURANETGTK_DESKTOP) $(prefix)/share/applications
	test -z '$(FACTURANET_FNNEGOCIO_DLL)' || cp $(FACTURANET_FNNEGOCIO_DLL) $(prefix)/lib/$(PACKAGE)
	test -z '$(FACTURANET_FNNEGOCIO_DLL_MDB)' || cp $(FACTURANET_FNNEGOCIO_DLL_MDB) $(prefix)/lib/$(PACKAGE)
	test -z '$(AMUTIL_DLL)' || cp $(AMUTIL_DLL) $(prefix)/lib/$(PACKAGE)
	test -z '$(AMUTIL_DLL_MDB)' || cp $(AMUTIL_DLL_MDB) $(prefix)/lib/$(PACKAGE)
	make post-install-local-hook prefix=$(prefix)

uninstall-local: $(ASSEMBLY) $(ASSEMBLY_MDB) $(FIREBIRDSQL_DATA_FIREBIRDCLIENT_DLL) $(FACTURANETGTK) $(FACTURANETGTK_DESKTOP) $(FACTURANET_FNNEGOCIO_DLL) $(FACTURANET_FNNEGOCIO_DLL_MDB) $(AMUTIL_DLL) $(AMUTIL_DLL_MDB)
	make pre-uninstall-local-hook prefix=$(prefix)
	rm -f $(prefix)/lib/$(PACKAGE)/$(notdir $(ASSEMBLY))
	test -z '$(ASSEMBLY_MDB)' || rm -f $(prefix)/lib/$(PACKAGE)/$(notdir $(ASSEMBLY_MDB))
	test -z '$(FIREBIRDSQL_DATA_FIREBIRDCLIENT_DLL)' || rm -f $(prefix)/lib/$(PACKAGE)/$(notdir $(FIREBIRDSQL_DATA_FIREBIRDCLIENT_DLL))
	test -z '$(FACTURANETGTK)' || rm -f $(prefix)/bin/$(notdir $(FACTURANETGTK))
	test -z '$(FACTURANETGTK_DESKTOP)' || rm -f $(prefix)/share/applications/$(notdir $(FACTURANETGTK_DESKTOP))
	test -z '$(FACTURANET_FNNEGOCIO_DLL)' || rm -f $(prefix)/lib/$(PACKAGE)/$(notdir $(FACTURANET_FNNEGOCIO_DLL))
	test -z '$(FACTURANET_FNNEGOCIO_DLL_MDB)' || rm -f $(prefix)/lib/$(PACKAGE)/$(notdir $(FACTURANET_FNNEGOCIO_DLL_MDB))
	test -z '$(AMUTIL_DLL)' || rm -f $(prefix)/lib/$(PACKAGE)/$(notdir $(AMUTIL_DLL))
	test -z '$(AMUTIL_DLL_MDB)' || rm -f $(prefix)/lib/$(PACKAGE)/$(notdir $(AMUTIL_DLL_MDB))
	make post-uninstall-local-hook prefix=$(prefix)

install: install-local
uninstall: uninstall-local
clean: clean-local

include $(top_srcdir)/rules.make
