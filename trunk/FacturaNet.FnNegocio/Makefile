
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
ASSEMBLY = ./bin/Debug/FacturaNet.FnNegocio.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../AmUtil/bin/Debug/AmUtil.dll
BUILD_DIR = ./bin/Debug

FIREBIRDSQL_DATA_FIREBIRDCLIENT_DLL_SOURCE=../lib/FirebirdSql.Data.FirebirdClient.dll
AMUTIL_DLL_SOURCE=../AmUtil/bin/Debug/AmUtil.dll
AMUTIL_DLL_MDB_SOURCE=../AmUtil/bin/Debug/AmUtil.dll.mdb
AMUTIL_DLL_MDB=$(BUILD_DIR)/AmUtil.dll.mdb

endif

ifeq ($(CONFIG),RELEASE)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4
ASSEMBLY = ./bin/Release/FacturaNet.FnNegocio.dll
ASSEMBLY_MDB = 
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../AmUtil/bin/Release/AmUtil.dll
BUILD_DIR = ./bin/Release

FIREBIRDSQL_DATA_FIREBIRDCLIENT_DLL_SOURCE=../lib/FirebirdSql.Data.FirebirdClient.dll
AMUTIL_DLL_SOURCE=../AmUtil/bin/Release/AmUtil.dll
AMUTIL_DLL_MDB=

endif


PROGRAMFILES = \
	$(FIREBIRDSQL_DATA_FIREBIRDCLIENT_DLL) \
	$(AMUTIL_DLL) \
	$(AMUTIL_DLL_MDB)  

LINUX_PKGCONFIG = \
	$(FACTURANET_FNNEGOCIO_PC)  



FIREBIRDSQL_DATA_FIREBIRDCLIENT_DLL = $(BUILD_DIR)/FirebirdSql.Data.FirebirdClient.dll
FACTURANET_FNNEGOCIO_PC = $(BUILD_DIR)/facturanet.fnnegocio.pc
AMUTIL_DLL = $(BUILD_DIR)/AmUtil.dll


FILES = \
	AssemblyInfo.cs \
	RolDb.cs \
	Sesion.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	facturanet.fnnegocio.pc.in 

REFERENCES =  \
	System \
	System.Data

DLL_REFERENCES =  \
	../lib/FirebirdSql.Data.FirebirdClient.dll

CLEANFILES += $(PROGRAMFILES) $(LINUX_PKGCONFIG) 

#Targets
all: $(ASSEMBLY) $(PROGRAMFILES) $(LINUX_PKGCONFIG)  $(top_srcdir)/config.make

$(FIREBIRDSQL_DATA_FIREBIRDCLIENT_DLL): $(FIREBIRDSQL_DATA_FIREBIRDCLIENT_DLL_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(FACTURANET_FNNEGOCIO_PC): facturanet.fnnegocio.pc
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(AMUTIL_DLL): $(AMUTIL_DLL_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(AMUTIL_DLL_MDB): $(AMUTIL_DLL_MDB_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'



facturanet.fnnegocio.pc: facturanet.fnnegocio.pc.in $(top_srcdir)/config.make
	sed -e "s,@prefix@,$(prefix)," -e "s,@PACKAGE@,$(PACKAGE)," < facturanet.fnnegocio.pc.in > facturanet.fnnegocio.pc


$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(build_resx_resources) : %.resources: %.resx
	resgen2 '$<' '$@'

$(ASSEMBLY) $(ASSEMBLY_MDB): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list)
	make pre-all-local-hook prefix=$(prefix)
	mkdir -p $(dir $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
	make post-all-local-hook prefix=$(prefix)


install-local: $(ASSEMBLY) $(ASSEMBLY_MDB) $(FIREBIRDSQL_DATA_FIREBIRDCLIENT_DLL) $(FACTURANET_FNNEGOCIO_PC) $(AMUTIL_DLL) $(AMUTIL_DLL_MDB)
	make pre-install-local-hook prefix=$(prefix)
	mkdir -p $(prefix)/lib/$(PACKAGE)
	cp $(ASSEMBLY) $(ASSEMBLY_MDB) $(prefix)/lib/$(PACKAGE)
	test -z '$(FIREBIRDSQL_DATA_FIREBIRDCLIENT_DLL)' || cp $(FIREBIRDSQL_DATA_FIREBIRDCLIENT_DLL) $(prefix)/lib/$(PACKAGE)
	mkdir -p $(prefix)/lib/pkgconfig
	test -z '$(FACTURANET_FNNEGOCIO_PC)' || cp $(FACTURANET_FNNEGOCIO_PC) $(prefix)/lib/pkgconfig
	test -z '$(AMUTIL_DLL)' || cp $(AMUTIL_DLL) $(prefix)/lib/$(PACKAGE)
	test -z '$(AMUTIL_DLL_MDB)' || cp $(AMUTIL_DLL_MDB) $(prefix)/lib/$(PACKAGE)
	make post-install-local-hook prefix=$(prefix)

uninstall-local: $(ASSEMBLY) $(ASSEMBLY_MDB) $(FIREBIRDSQL_DATA_FIREBIRDCLIENT_DLL) $(FACTURANET_FNNEGOCIO_PC) $(AMUTIL_DLL) $(AMUTIL_DLL_MDB)
	make pre-uninstall-local-hook prefix=$(prefix)
	rm -f $(prefix)/lib/$(PACKAGE)/$(notdir $(ASSEMBLY))
	test -z '$(ASSEMBLY_MDB)' || rm -f $(prefix)/lib/$(PACKAGE)/$(notdir $(ASSEMBLY_MDB))
	test -z '$(FIREBIRDSQL_DATA_FIREBIRDCLIENT_DLL)' || rm -f $(prefix)/lib/$(PACKAGE)/$(notdir $(FIREBIRDSQL_DATA_FIREBIRDCLIENT_DLL))
	test -z '$(FACTURANET_FNNEGOCIO_PC)' || rm -f $(prefix)/lib/pkgconfig/$(notdir $(FACTURANET_FNNEGOCIO_PC))
	test -z '$(AMUTIL_DLL)' || rm -f $(prefix)/lib/$(PACKAGE)/$(notdir $(AMUTIL_DLL))
	test -z '$(AMUTIL_DLL_MDB)' || rm -f $(prefix)/lib/$(PACKAGE)/$(notdir $(AMUTIL_DLL_MDB))
	make post-uninstall-local-hook prefix=$(prefix)

install: install-local
uninstall: uninstall-local
clean: clean-local

include $(top_srcdir)/rules.make
